#light

namespace Storm.Types.WebHttp

   open System
   open System.IO
   open System.Net
   open Storm.Utilities.GenHelper
   open Storm.Types.Configuration
   
   
   type HttpResponseData = 
     {
        Response : string;
        StatusCode : int; 
        StatusDescription : string;
        ContentLength : int64;
        ContentType : string
        TimeReceived : DateTime;
        Server : string
        HttpReqMethod : string
     }
        
   type HttpRequestData = 
     { 
         Request: string ; 
         TimeSent : DateTime; 
         WebRequestConfig : WsConfigItems
     }