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
   open System.Xml.Serialization
   open System.Threading
   open Microsoft.CSharp
   open Storm.Types
   open Storm.Types.WebSvcImpl
   open Storm.Types.Configuration
   open Storm.Types.CodeGenImpl
   open Storm.Types.WsdlLoaderImpl
   open Storm.Utilities
   open Storm.Utilities.LogHelper
   open Storm.Utilities.GenHelper
   open Storm.CS.Lib.Soap

   //
   // Creates the websevice model
   //
   [<AbstractClass>]
   type IWebSvcManager(url:string) =
      let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
      
        
      abstract WsdlEndpoint : string
      abstract Proxy : string
      abstract Download : unit -> bool
      abstract GenerateProxy : unit -> bool
      abstract CompileProxy : unit->bool
      abstract CreateWebServiceModel : unit -> IWebSvc
      
       //Implementation of ILog interface
      interface ILog with       
         member x.LogEvt = (logTrigger, logEvent) 
         
   module WebSvcManagerOps =
      let createWebSvcManager url (configOpt:MiscConfigItems option) : IWebSvcManager= 
         
        let logTrigger = ref ( fun (m:LogMessage) -> () )
        let serviceName = ref ""
        
        let wsdlLoader =
         let temp = createWsdlLoader url configOpt 
         let _,loaderEvts = (temp :> ILog).LogEvt
         loaderEvts.Add(fun msg -> !logTrigger msg )
         temp
        
        let codeMgr =
         let temp = createCodeGen()
         let _,codeEvts = (temp :> ILog).LogEvt
         codeEvts.Add(fun msg -> !logTrigger msg )
         temp
        
             
        {
           new  IWebSvcManager(url) with
             member x.WsdlEndpoint 
               with get() = url
           
             //
             // Discover the wsdlEndpoint and get the
             // service descriptions
             //
             member x.Download() =
               logTrigger :=  (x :> ILog).LogEvt |> fst
               wsdlLoader.Load()
              // if (wsdlLoader.ServiceDescriptions <> None) then
               if (List.length wsdlLoader.ServiceDescriptions > 0) then
                 //let svcs = wsdlLoader.ServiceDescriptions |> Option.get |> snd
                 let temp = List.hd (wsdlLoader.ServiceDescriptions )
                 serviceName := temp.Services.Item(0).Name
               true 
             
             //
             //  Generates the proxy code (for the moment the code 
             //     generated is in C#)
             //
             member x.GenerateProxy() = 
               logTrigger :=  (x :> ILog).LogEvt |> fst
               codeMgr.CreateSource(wsdlLoader)
             member x.Proxy 
               with get () = codeMgr.ClientSourceCode
             
             //
             // Compiles the generated code
             //   
             member x.CompileProxy() = 
               codeMgr.CompileSource(x.Proxy)
               true
               
             //
             //  Creates the IWebSvc model
             //
             member x.CreateWebServiceModel() =
               if codeMgr.CompiledAssembly <> None then
                  let assem = Option.get codeMgr.CompiledAssembly
                  //let wsdlString = (Option.get wsdlLoader.ServiceDescriptions) |> fst   
                 // let defSoapMsg = 
                  
                  let wsdlString =wsdlLoader.WsdlContent
                  
                  let scvDescColl =    
                     let temp = new ServiceDescriptionCollection()              
                     wsdlLoader.ServiceDescriptions |> List.iter(fun sdesc -> temp.Add(sdesc) |> ignore ) 
                     temp
                  let defSoapMsg =
                    let temp = (new StormSoapMessages(scvDescColl)).GetDefaultSoapMessages(false)
                    [for n in temp -> n.Key,n.Value]
                  
                  if List.length defSoapMsg <= 0 then
                    !logTrigger { LogType = LogTypeEnum.Warning ; Message = "Unable to create default soap messages" }
                  
                  let webSvc = createWebSvc codeMgr.WsdlEndpoint !serviceName  wsdlString  assem defSoapMsg
                 
                  let _, iWebSvcEvt = (webSvc:> ILog).LogEvt
                  iWebSvcEvt.Add(fun s -> !logTrigger s)
                              
                  let ok = webSvc.ReadAssembly()
                  if ok then
                     webSvc
                  else
                     failwith "Failed to read assembly"
               else
                 failwith "Assembly not yet created."

        
        }
     