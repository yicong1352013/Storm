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
   open System.Web.Services
   open System.Web.Services.Protocols
   open System.Web.Services.Description
   open System.Web.Services.Discovery
   open System.Windows.Forms
   open System.Xml
   open System.Net
   open System.Reflection
   open System.Xml.Schema
   open System.Xml.Serialization
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigurationImpl
   open Storm.CS.Lib.Soap
   
   [<AbstractClass>]    
   type IWebSvc(url:string, serviceName: string , wsdl:string , assm:Assembly, defSoapMsgs:list<string*StormDefaultSoap>) = 
      let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
      let webNodeReszArr = new ResizeArray<WebMethNode2 >()
      
      let defaultLastOutput = 
       {
         TvInputNode = new TreeNode(); 
         TvOutputNode = new TreeNode();
         SoapResp = "";
         SoapReq = ""
         RequestHead = ""
       }
       
      
       
      //Webservice method name , MethodInfo
      abstract ReflectedWebMethods : (string * MethodInfo) list
      abstract ReadAssembly : unit -> bool
     // abstract InvokeWebMethod : WebMethodInvoke -> (MethodInfo -> string) -> WebMethodInvoke
      abstract ConfiguredClientProxy : MiscConfigItems option-> HttpWebClientProtocol
         
      member x.DefaultSoapMessages = defSoapMsgs
      member x.AddWebMethodNode(wmNode:WebMethNode2) =
         let tempOpt = webNodeReszArr |> ResizeArray.tryfind (fun wm -> wm.Name = wmNode.Name)
         if tempOpt.IsNone then
           webNodeReszArr.Add(wmNode)
           
      member x.GetWebMethodNode(name:string) =
         webNodeReszArr |> ResizeArray.tryfind (fun n -> n.Name = name)
      
      member x.GenerateMethodId(methodname) =
       String.Format("{0}_Storm_{1}", x.Url, methodname)
       
      member x.GetAllWebserviceMethods() =
        x.ReflectedWebMethods |> List.map (fun (n,_)->n)
     
      member x.ServiceName 
        with get() = serviceName
      member x.Wsdl 
        with get() = wsdl          
      member x.Url 
        with get() = url
      member x.Assembly 
        with get() = assm
     // default x.SvcConfiguration = defaultConfig 
     
      //Implementation of ILog interface
      interface ILog with       
         member x.LogEvt = (logTrigger, logEvent)
   
   module WebSvcImpl =
     
      let isWebService ty=
         (typeof<HttpWebClientProtocol>).IsAssignableFrom(ty)
      let isWebMethod (mi:MethodInfo) =      
           let rec test li  =
             match li with 
             | h::tail ->
                let temp = mi.GetCustomAttributes(h, true)
                if temp <> null && temp.Length > 0 then
                  true
                else
                  test tail
             | [] -> false
                
           test [typeof<SoapRpcMethodAttribute>; typeof<SoapDocumentMethodAttribute>;typeof<HttpMethodAttribute> ]
      
      let mutable logTrigger = fun logMsg -> ()
      let createWebSvc url serviceName wsdl (assm:Assembly) defSoapMsg= 
             
        let typesInAssembly = List.of_array (assm.GetTypes() )
        let reflectedInfo = ref [("",null) ]
        
        let readAssembly (assm) = 
            logTrigger ( logInfo ("Analyzing the compiled assembly generated from " + url) )
            let methList = 
              typesInAssembly 
                |> List.filter (fun y -> isWebService y)
                |> List.map (fun typ -> 
                  let methodsList = List.of_array( typ.GetMethods() )
                  let webMethods = methodsList |> List.choose (fun meth -> 
                       if isWebMethod(meth) then
                         logTrigger ( logInfo (String.Format("Found method : {0} from type {1}", meth.Name, (typ.ToString()) )) )
                         Some (meth.Name, meth)
                       else
                         None
                    )
                  
                  webMethods
                            
               ) 
            methList |> List.concat
        
        //Object Expression         
        {
           new IWebSvc(url, serviceName, wsdl, assm, defSoapMsg) with
                                       
              member x.ReadAssembly() = 
                try
                   logTrigger <- (fst (x :> ILog).LogEvt)
                   
                   reflectedInfo := readAssembly x.Assembly            
                   true
                with
                | exc ->  
                   logTrigger (logErr (exc.ToString()))                 
                   false
              
              member x.ReflectedWebMethods 
                 with get () = 
                   let temp = (!reflectedInfo) |> List.filter (fun x -> (fst x) <> "")                   
                   if (List.length temp) >= 1 then
                     temp
                   else
                     x.ReadAssembly() |> ignore
                     x.ReflectedWebMethods
                    
              //Default Proxy. Not using any Credentials       
              member x.ConfiguredClientProxy (miscCfg: MiscConfigItems option) =                 
                    logTrigger <- (fst (x :> ILog).LogEvt)
                    let wsType = typesInAssembly |> List.find (fun y -> isWebService y)
                       
                    let inst : HttpWebClientProtocol = 
                       let temp : HttpWebClientProtocol= cast (Activator.CreateInstance(wsType))
                       temp.AllowAutoRedirect <-true
                       temp.CookieContainer <- new CookieContainer()
                       
                       if miscCfg <> None then
                          let credentialsOpt, proxyOpt =  miscCfg |> ConfigReaderImpl.readMiscCfg 
                    
                          if credentialsOpt <> None then
                            temp.Credentials <- credentialsOpt |> Option.get
                          if proxyOpt <> None then
                            temp.Proxy <- proxyOpt |> Option.get   
                       temp
                    inst
          
        }