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
   open System.Reflection
   open System.Net
   open System.IO
   open System.Text
   open System.Web.Services.Protocols
   open Storm.Types
   open Storm.Utilities.GenHelper
   open Storm.Types.Configuration
   open Storm.Types.WebHttp   

   module MethodInvokerOps = 
   
      let invokeViaReflection proxy (curMethodInfo : MethodInfo)=
        fun  methodParams -> 
             curMethodInfo.Invoke(proxy, BindingFlags.Public, null, methodParams, null)
  
      let private setRequestProps (request:DelegatingHttpReq) (wsCfg:WsConfigItems) =
        request.Method <- wsCfg.Method
        request.ContentType <- wsCfg.ContentType
        request.Headers.Item("SOAPAction") <- wsCfg.SoapAction
        request.SendChunked <- wsCfg.SendChunked
        request.AllowAutoRedirect <- wsCfg.AllowAutoRedirect
        request.AllowWriteStreamBuffering <- wsCfg.AllowAutoRedirect
        request.KeepAlive <- wsCfg.KeepAlive
        request.Pipelined <- wsCfg.Pipelined
        request.PreAuthenticate <- wsCfg.PreAuthenticate   
        request
        
      let invokeViaHttp (requestXml:string) (miscCfg:MiscConfigItems) (wsCfg:WsConfigItems) (proxy:HttpWebClientProtocol)=  
         let request:HttpWebRequest = cast (WebRequest.CreateDefault(new Uri(wsCfg.URL)))   
         
         let temp : DelegatingHttpReq = DelegatingHttpReq.Create(new Uri(wsCfg.URL)) |> cast
         
         let setRequest = setRequestProps temp wsCfg
         setRequest.Timeout <- miscCfg.SoapCallTimeout
         let credentialsOpt, proxyOpt = Some(miscCfg) |> ConfigReaderImpl.readMiscCfg 
         if proxyOpt <> None then setRequest.Proxy <- proxyOpt |> Option.get
         
         if credentialsOpt <> None then setRequest.Credentials <- credentialsOpt |> Option.get
         
         use sw = new StreamWriter(setRequest.GetRequestStream(), Encoding.UTF8 )
         sw.Write(requestXml)
         sw.Flush()
         use resp = setRequest.GetResponse()
         
         //Note : In the ui.formmain.fs we registered a custom handler (see "creator" value ) for the http prefix.
         //       That handler already has events listening to the request and response events published
         //       by the  DelegatingHttpReq class, as such we dont do anything to the output of setRequest.GetResponse()
         ()
  