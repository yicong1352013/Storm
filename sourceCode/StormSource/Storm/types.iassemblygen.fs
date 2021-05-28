//Copyright (c) 2008, Erik Araojo
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and 
//the following disclaimer in the documentation and/or other materials provided with the distribution.
//* Neither the name of Erik Araojo nor the names of its contributors may be used to endorse or 
//promote products derived from this software without specific prior written permission.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
//IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
//FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
//LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
//THE POSSIBILITY OF SUCH DAMAGE. 

#light

namespace Storm.Types

   open System
   open System.CodeDom;
   open System.CodeDom.Compiler;
   open System.Data
   open System.Collections
   open System.IO
   open System.Text
   open System.Net
   open System.Reflection
   open System.Runtime.InteropServices
   open System.Web.Services
   open System.Web.Services.Description
   open System.Web.Services.Discovery
   open System.Xml
   open System.Xml.Schema
   open System.Xml.Serialization
   open System.Threading
   open Microsoft.CSharp
   open Storm.Types
   open Storm.Utilities
   open Storm.Utilities.LogHelper
   open Storm.Utilities.GenHelper
   
   [<AbstractClass>]
   type IAssemblyGen() =
      let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
      
      abstract  GenerateCode : IWsdlLoader -> string
      abstract CompileSource : string -> Assembly option
         
      //Implementation of ILog interface
      interface ILog with       
         member x.LogEvt = (logTrigger, logEvent)
   
   
   module CodeGenImpl =
   
      
      let cSharpCodeGen (svcDesc :list<ServiceDescription>, xsdList: XmlSchema list) =            
          
         let servImport = new ServiceDescriptionImporter()
         let rec addDesc (srvI:ServiceDescriptionImporter) l =
            match l with
            | h::tail -> 
               srvI.AddServiceDescription(h, "","")
               addDesc srvI tail
            | [] -> srvI
         let servImport2 = addDesc servImport svcDesc
         //let servImport2 = 
            //let lastPos = (List.length svcDesc) -1
            //servImport.AddServiceDescription ( ( List.nth svcDesc 0) ,"", "" )
            //servImport
         
         xsdList |> List.iter (fun xsd -> servImport2.Schemas.Add(xsd) |> ignore )
         servImport2.Style <- ServiceDescriptionImportStyle.Client;
         servImport2.ProtocolName <- "Soap"
         //servImport.CodeGenerationOptions <- CodeGenerationOptions.None
         servImport2.CodeGenerationOptions <- CodeGenerationOptions.GenerateProperties
         
         //TODO: Grab this from the web service namespace
         //let codeNs = new CodeNamespace("Storm")
         let codeNs = new CodeNamespace("")
         let ccUnit = new CodeCompileUnit()
         ccUnit.Namespaces.Add(codeNs) |> ignore
         
         let warnings =  servImport2.Import(codeNs, ccUnit)  
         
         let createdCSharpSource = 
           match warnings with 
           | ServiceDescriptionImportWarnings.NoCodeGenerated | ServiceDescriptionImportWarnings.NoMethodsGenerated -> ""
           | _ ->
              //F# Gods please don't curse me for generating C# code. :(  
               let csProv = new CSharpCodeProvider()
               let sw = new StringWriter()
               csProv.GenerateCodeFromNamespace(codeNs, sw, null);
               sw.ToString()
          
         
         createdCSharpSource
         
         
      type AssemblyFromCode() =
         let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
         let mutable _code = ""
         let mutable _assembly = None
         let _references = [| "System.dll"; "System.Xml.dll"; "System.Web.Services.dll"; "System.Data.dll" |]
         let mutable url = ""
         
         //This Code/Assebly generator creates C# source code.
         //Create a new implementation of IAssemblyGen if you like to generate code 
         //using another language.
         let asCSharpGenerator =
           {
               new IAssemblyGen() with
                 member x.GenerateCode (wsdLoad) =
                   url <- wsdLoad.WsdlEndpoint
                   //let trigger,_ = (x :> ILog).LogEvt
                   
                   logTrigger (logInfo "Generating code...")
                    
                   //if wsdLoad.ServiceDescriptions <> None then
                   if List.length wsdLoad.ServiceDescriptions > 0 then
                      //let svDesc = snd (Option.get wsdLoad.ServiceDescriptions) 
                      
                      
                      let code = cSharpCodeGen (wsdLoad.ServiceDescriptions , wsdLoad.SchemasList)
                      
                      //trigger (logDebug ( String.Format( "SvcDescription Length = {0}", (List.length svDesc)) ) )             
                      //trigger (logDebug code)
                      
                      
                      if code <> "" then logTrigger(logSuccess "Code generated successfully.")
                      else logTrigger(logErr "Code generation failed.")
                      code
                   else
                      logTrigger (logErr "ServiceDescriptions is Empty")
                      ""
                   
                 member x.CompileSource(code) =
                  
                   try
                      
                      if String.IsNullOrEmpty(code) then
                          None
                      else
                         let prov = new CSharpCodeProvider();

                         let param = new CompilerParameters(_references)
                         param.GenerateExecutable <- false
                         param.GenerateInMemory <- false
                         param.TreatWarningsAsErrors <- false
                         param.WarningLevel <- 4
                      
                         logTrigger(logInfo "Compiling into an assembly...")
                         let results:CompilerResults = prov.CompileAssemblyFromSource(param, [| code |] )   
   
                              
                         if results.Errors.HasErrors then
                            for n in results.Errors do
                              logTrigger (logErr (n.ToString()))
                            None
                         else
                            Some (results.CompiledAssembly)
                      
                      
                   with
                   | exc ->                                           
                       logTrigger (logErr (exc.ToString()))
                       None
        
        
           }
       
            
         
        // do evt.Add(fun x -> logTrigger x)
         
         member x.WsdlEndpoint 
           with get () = url           
         member x.ClientSourceCode
            with get () = _code
         member x.CompiledAssembly 
           with get () =  _assembly
         member x.CreateSource(ws :IWsdlLoader ) = 
            _code <- asCSharpGenerator.GenerateCode(ws)
            _code <> ""
         member x.CompileSource(code) = 
           _assembly <- asCSharpGenerator.CompileSource(code) 
         //Implementation of ILog interface
         
         interface ILog with       
            member x.LogEvt =(logTrigger, logEvent)
                    
      let createCodeGen() = new AssemblyFromCode()
      
         
            
        