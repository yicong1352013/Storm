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

namespace Storm.Types.WebHttp

   open System
   open System.IO
   open System.Threading
   open System.Net
   open Storm.Utilities.GenHelper
   open Storm.Types.Configuration
   open Storm.Types.WebHttp
   
   
  
   type DelegatingHttpReq(httpReq : HttpWebRequest) = 
        inherit WebRequest()
                     
        let mutable reqStreamPopulated:OpenMemStream = new OpenMemStream()
        let mutable requestAsString :string = ""
        let triggerRequest, (requestEvent:IEvent<HttpRequestData> )= Event.create()
        let triggerResponse, ( responseEvent :IEvent<HttpResponseData> )= Event.create()
        
        member x.RequestEvent = requestEvent
        member x.ResponseEvent = responseEvent
        
        member x.SendChunked
           with get() = httpReq.SendChunked
           and set s = httpReq.SendChunked <-s
        member x.AllowAutoRedirect
           with get() = httpReq.AllowAutoRedirect
           and set s = httpReq.AllowAutoRedirect <-s
        member x.AllowWriteStreamBuffering
          with get() = httpReq.AllowWriteStreamBuffering
           and set s = httpReq.AllowWriteStreamBuffering <-s
        member x.Pipelined
          with get() = httpReq.Pipelined
           and set s = httpReq.Pipelined <-s
        member x.KeepAlive
          with get() = httpReq.KeepAlive
           and set s = httpReq.KeepAlive <-s
      
        
        override x.ConnectionGroupName
           with get() = httpReq.ConnectionGroupName
           and set s = httpReq.ConnectionGroupName <-s

        override x.ContentLength
           with get() = httpReq.ContentLength
           and set s = httpReq.ContentLength <-s
           

        override x.ContentType
           with get() = httpReq.ContentType
           and set s = httpReq.ContentType <-s
           
          

        override x.Credentials
           with get() = httpReq.Credentials
           and set s = httpReq.Credentials <-s
           
        override x.Headers
           with get() = httpReq.Headers
           and set s = httpReq.Headers <-s
           
        override x.Method
           with get() = httpReq.Method
           and set s = httpReq.Method <-s
           
        override x.PreAuthenticate
           with get() = httpReq.PreAuthenticate
           and set s = httpReq.PreAuthenticate <-s
           

        override x.Proxy
           with get() = httpReq.Proxy
           and set s = httpReq.Proxy <-s

        override x.RequestUri
           with get() = httpReq.RequestUri
           
        override x.Timeout
           with get() = httpReq.Timeout
           and set s = httpReq.Timeout <- s  
        
        
        override x.BeginGetRequestStream (callback,asyncState) =
           httpReq.BeginGetRequestStream(callback, asyncState)
        
        override x.BeginGetResponse(callback, asyncState) =
            httpReq.BeginGetResponse(callback, asyncState)

        override x.EndGetRequestStream(result) =
            httpReq.EndGetRequestStream(result)

        override x.EndGetResponse(result) =
            httpReq.EndGetResponse(result)
        override x.GetRequestStream() =
            (reqStreamPopulated :> Stream)
            //httpReq.GetRequestStream()
        override x.GetResponse() =
           
           let defaultWsCfg = ConfigurationImpl.defaultWsConfig
           
           defaultWsCfg.ContentType <- httpReq.ContentType
           defaultWsCfg.AllowAutoRedirect <- httpReq.AllowAutoRedirect
           defaultWsCfg.KeepAlive <- httpReq.KeepAlive
           defaultWsCfg.Method <- httpReq.Method.ToString()
           defaultWsCfg.Pipelined <- httpReq.Pipelined
           defaultWsCfg.PreAuthenticate <- httpReq.PreAuthenticate
           defaultWsCfg.SendChunked <- httpReq.SendChunked 
           defaultWsCfg.SoapAction <-  httpReq.Headers.Item("SOAPAction")
           defaultWsCfg.URL <- httpReq.RequestUri.ToString()
           
           //defaultWsCfg.UseCookieContainer <- httpReq.U
           
           if httpReq.Method.ToLower() = "post" then
           
             using (httpReq.GetRequestStream()) ( fun connectStream ->
                   connectStream.Write(reqStreamPopulated.GetBuffer(), 0, Convert.ToInt32( reqStreamPopulated.Length))
                   connectStream.Flush()
                   reqStreamPopulated.Seek(0L, SeekOrigin.Begin) |> ignore
                )
             (* use connectStream = httpReq.GetRequestStream()
              connectStream.Write(reqStreamPopulated.GetBuffer(), 0, Convert.ToInt32( reqStreamPopulated.Length))
              reqStreamPopulated.Seek(0L, SeekOrigin.Begin) |> ignore*)
            
           use reader = new StreamReader(reqStreamPopulated)
           requestAsString <- reader.ReadToEnd()       
           reqStreamPopulated.Seek(0L, SeekOrigin.Begin) |> ignore
           
           let httpReqData = 
             {
                Request = requestAsString; 
                TimeSent = DateTime.Now ;  
                WebRequestConfig=defaultWsCfg 
             }
             
           triggerRequest(httpReqData)
          
           let webRespBase() =
             try
               let temp = httpReq.GetResponse()
               if temp <> null then Some(temp)
               else None
             with
             | :? WebException  as exc-> 
               if exc.Response <> null then Some(exc.Response)
               else failwith(exc.ToString())
             
            
           let httpWbresp : HttpWebResponse = 
              match webRespBase() with 
              | Some(w) -> 
                 let temp : HttpWebResponse = cast (w )
                 temp
              | None -> failwith "Web service response was empty"
              
           let webResp = new DelegatingHttpResp(httpWbresp)
           
           //TODO : Assign CookieCOntainer
           let httpRespData = 
               {

                 Response = webResp.ResponseString;
                 StatusCode =Convert.ToInt32( webResp.StatusCode );
                 StatusDescription = webResp.StatusDescription;
                 ContentLength = webResp.ContentLength;
                 ContentType = webResp.ContentType;
                 TimeReceived = DateTime.Now;
                 Server = webResp.Server;
                 HttpReqMethod = httpReq.Method.ToLower()            
               }
               
           triggerResponse( httpRespData )

           webResp :> WebResponse
           
         
         
   type StormReqCreate() =
      let reqTrigger, (requestEvent:IEvent<HttpRequestData> )= Event.create()
      let respTrigger, ( responseEvent :IEvent<HttpResponseData> )= Event.create()
        
      let syncContext = SynchronizationContext.Current
      let reqTriggerParent msg= 
        syncContext.Post(SendOrPostCallback (fun _ -> 
            reqTrigger msg 
            ),null)
      let respTriggerParent msg= 
        syncContext.Post(SendOrPostCallback (fun _ -> 
            respTrigger msg 
            ),null)
                
      member x.RequestEvent = requestEvent
      member x.ResponseEvent = responseEvent
      interface IWebRequestCreate with
         override x.Create(uri) =
            let httpWr : HttpWebRequest = cast (WebRequest.CreateDefault(uri) )
            
            //Some of request don't work without this, especially if the ISPs is messing with the connection
            httpWr.Accept <- "*/*"  
            let temp = new DelegatingHttpReq(httpWr)
            
            let wscfg = ConfigurationImpl.defaultWsConfig
            let miscCfg= ConfigurationImpl.defaultConfig
            
            temp.Timeout <- miscCfg.SoapCallTimeout
            temp.AllowAutoRedirect <- wscfg.AllowAutoRedirect
            temp.AllowWriteStreamBuffering <- wscfg.AllowAutoRedirect
            temp.KeepAlive <- wscfg.KeepAlive
            temp.Pipelined <- wscfg.Pipelined
            temp.PreAuthenticate <- wscfg.PreAuthenticate
            temp.SendChunked <- wscfg.SendChunked
            
            
            temp.RequestEvent.Add(fun data -> reqTriggerParent data)
            temp.ResponseEvent.Add(fun data -> respTriggerParent data)
           
            temp :> WebRequest
      

   module StormReqCreateOps =
     let createCustomHttpListener =  new StormReqCreate()
        
     let registerPrefix(creator)=
        WebRequest.RegisterPrefix("http://", creator) |> ignore     
        WebRequest.RegisterPrefix("https://", creator) |> ignore     