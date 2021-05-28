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
   open System.CodeDom;
   open System.CodeDom.Compiler;
   open System.Data
   open System.Collections
   open System.IO
   open System.Net
   open System.Reflection
   open System.Runtime.InteropServices
   open System.Web.Services
   open System.Web.Services.Description
   open System.Web.Services.Discovery
   open System.Xml
   open System.Xml.Schema
   open System.Xml.Serialization
   open Storm.Types.Configuration
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   open Storm.Types
    
   [<AbstractClass>] 
   type IWsdlLoader(wsdlEndpoint :string) =
      let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
      let mutable _wsdlEndpoint = wsdlEndpoint
      let mutable (_svcDescriptions: (string * ServiceDescription list) option) = None
      let mutable _wsdlContent = ""
      let  _schemaColl = new ResizeArray<XmlSchema>()
      let _svcdescriptionColl = new ResizeArray<ServiceDescription>();
      //abstract Load : (string->string * list<ServiceDescription>) -> unit
      abstract Load : unit -> unit
      
      
      member x.WsdlContent 
        with get () = _wsdlContent
        and set s = _wsdlContent <- s
      member x.WsdlSchemaSet 
        with get () =
          if _schemaColl.Count  <= 0 then
            None
          else
            let setXsd = new Schema.XmlSchemaSet()
            for x in _schemaColl do
               setXsd.Add(x) |> ignore
               setXsd.Reprocess(x) |> ignore
               
            setXsd.Compile()
            Some(setXsd)
               
      member x.WsdlEndpoint 
        with get () = _wsdlEndpoint
      member x.SchemasList
        with get () = ResizeArray.to_list _schemaColl
      member x.ServiceDescriptions  
         //with get () = _svcDescriptions
         //and set s = _svcDescriptions <- s
         with get() = ResizeArray.to_list _svcdescriptionColl
      member x.AddServiceDescription(svc) =
        _svcdescriptionColl.Add(svc)
      member x.AddSchema(xsd) =
        _schemaColl.Add(xsd)
        
      
        
      //Implementation of ILog interface
      interface ILog with       
         member x.LogEvt = (logTrigger, logEvent)
      
   module WsdlLoaderImpl =
      
      let createWsdlLoader (wsdlEndpoint:string) (configOpt:MiscConfigItems option)= 
         
         let credentialsOpt, proxyOpt = ConfigReaderImpl.readMiscCfg configOpt
      
         let logTrigger = ref (fun logMsg -> () )
      
         // We need to force the creation of a new discoClient everytime
         // otherwise the methods of the previously added web services
         // will whos up in this current WS that we are discovering
         let discoClient() =
           !logTrigger ( logInfo "Acquiring the wsdl..." )
           use dclient = new DiscoveryClientProtocol()
           dclient.AllowAutoRedirect <- true
           dclient.Timeout <- 100000
          
           if credentialsOpt <> None then dclient.Credentials <- credentialsOpt |> Option.get
           if proxyOpt <> None then dclient.Proxy <- Option.get proxyOpt
           
           dclient
 
         let wsdlLoadFunction (wsdlEndpoint:string) =           
         
           use client = discoClient()
           client.Documents.Clear()
           let discoDoc = client.DiscoverAny(wsdlEndpoint)
           !logTrigger (logInfo "Resolving the wsdlendpoint...")
           client.ResolveAll()
           
           let wsdocs = [for i in client.Documents -> i]
           
           let dicEntries = 
             wsdocs 
             |> List.choose (fun theDoc -> 
                   let de:DictionaryEntry = cast theDoc
                   if de.Value.GetType().Name = "ServiceDescription" || de.Value.GetType().Name = "XmlSchema" then
                     Some(de)
                   else
                     None
                     
                )
           
           let svc, schemas =
             dicEntries 
             |> List.partition (fun entry -> entry.Value.GetType().Name = "ServiceDescription")
          
           let svcList =  svc |> List.map (fun sDesc ->  sDesc.Value :?> ServiceDescription) 
           let xsdList =  schemas |> List.map (fun xsd -> xsd.Value :?> XmlSchema)
           
           
           
          (* let svcList = 
              [ for i in client.Documents ->
                let o =  (i :?> DictionaryEntry).Value 
                match o with
                | :? ServiceDescription -> (o :?> ServiceDescription)
                //| "XmlSchema" -> (o :?> XmlSchema)
                | _ -> null
              ] 
              |> List.filter (fun x -> x <> null)
            *)
            
           //let svc
           use ms = new MemoryStream()
          
           //(List.nth svcList 0).Write(ms)
           svcList.Head.Write(ms)
           let wsdlContent = streamToString (ms :> Stream)    
           (wsdlContent , svcList, xsdList)
      
      
         {
            new IWsdlLoader(wsdlEndpoint) with
               member x.Load() =
                  logTrigger := (fst (x :> ILog).LogEvt)
                  try   
                     let wsdl, serviceDescriptions, schemas = wsdlLoadFunction (x.WsdlEndpoint)
                     x.WsdlContent <- wsdl
                     //x.ServiceDescriptions <- Some (wsdl,  serviceDescriptions)
                     
                     serviceDescriptions |> List.iter (fun sDesc ->  x.AddServiceDescription(sDesc))
                     // x.SchemasList
                     schemas |> List.iter (fun xsd -> x.AddSchema(xsd))
                     
                  with
                  | exc -> 
                      //x.ServiceDescriptions <- None
                      (fst (x :> ILog).LogEvt) (logErr (exc.ToString()))
              
         
         }
         
