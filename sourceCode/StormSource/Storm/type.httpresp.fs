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
   open System.Net
   open Storm.Utilities.GenHelper
   open Storm.Types.WebHttp
    
   type DelegatingHttpResp(httpResp : HttpWebResponse) = 
        inherit WebResponse()
       
        let mutable respStreamPopulated:OpenMemStream = 
          let temp = httpResp.GetResponseStream()
          let opStream= new OpenMemStream()
          let buffer = Array.create 323768 (new byte())
          let rec copy() =
            let count =  temp.Read(buffer, 0, buffer.Length)
            if count > 0 then
              opStream.Write(buffer, 0, count)
              copy()
            else
              ()
          copy()
          
          opStream.Seek(0L, SeekOrigin.Begin)   |> ignore
          opStream
          
        let mutable respAsString :string = 
          //let temp = new MemoryStream()
          //temp.Write(respStreamPopulated.GetBuffer(), 0, Convert.ToInt32( respStreamPopulated.Length))
          respStreamPopulated.Seek(0L, SeekOrigin.Begin)   |> ignore
          use sr = new StreamReader(respStreamPopulated)
          let s = sr.ReadToEnd()
          
          respStreamPopulated.Seek(0L, SeekOrigin.Begin)   |> ignore
          s
       
        
       
        override x.ContentLength
           with get() = httpResp.ContentLength
           and set s = httpResp.ContentLength <-s
           

        override x.ContentType
           with get() = httpResp.ContentType
           and set s = httpResp.ContentType <-s
           
     
        override x.Headers
           with get() = httpResp.Headers

        member x.ResponseString 
          with get() = respAsString
              
        member x.StatusCode
           with get() = httpResp.StatusCode
        member x.Server
           with get() = httpResp.Server
             
           
        member x.StatusDescription
           with get() = httpResp.StatusDescription

 
        override x.ResponseUri
           with get() = httpResp.ResponseUri
       
        
        override x.Close () =
           httpResp.Close()
        
        override x.GetResponseStream() =
            ( respStreamPopulated :> Stream)

