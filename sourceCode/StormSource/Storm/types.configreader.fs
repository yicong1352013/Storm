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

namespace Storm.Types.Configuration

   open System
   open System.Configuration
   open System.IO
   open System.Net
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigurationImpl
   open Storm.Utilities.GenHelper
   
   type ConfigReader() =
      abstract Read : (string -> 'b)  -> string -> 'b
      
      default x.Read f (name:string) =
         f name
   
   module ConfigReaderImpl = 
   
      
      let readMiscCfg (configOpt:MiscConfigItems option)= 
        
        let config =
           if configOpt = None then ConfigurationImpl.defaultConfig
           else Option.get configOpt
           
        let clientCredentials = 
              if config.WebServiceUseDefaultCredentials then
                 Some(CredentialCache.DefaultCredentials)
              elif  config.WebServiceUsername <> "" && config.WebServicePassword <> "" && config.WebServiceDomain <> "" then
                 let u = config.WebServiceUsername
                 let p = config.WebServicePassword
                 let d = config.WebServiceDomain
                 Some(new NetworkCredential(u,p,d) :> ICredentials )
              else  //Anonymous
                 None
        
        let proxyOpt =
             if config.ProxyServer <> "" then
                let temp = new WebProxy(config.ProxyServer)
                let proxyCredentials = 
                    if config.ProxyUseDefaultCredentials then
                       CredentialCache.DefaultCredentials
                    else
                       let u = config.ProxyUsername
                       let p = config.ProxyPassword
                       let d = config.ProxyDomain
                       new NetworkCredential(u,p,d) :> ICredentials
                temp.Credentials <- proxyCredentials
                Some(temp)
             else
                None
                
        clientCredentials, proxyOpt 
      
      let readAppSettings (keyname:string)=
      
         //Some(System.Configuration.ConfigurationManager.AppSettings.Item(keyname))
         try
            match System.Configuration.ConfigurationManager.AppSettings.[keyname] with
            | null -> None
            | temp -> Some(temp)
         with
         |_ -> None
         
      let createAppSettingsReader = new ConfigReader()
      
      let defaultsXslFile = Path.Combine(exePath , "defaultss.xsl")
      //readAppSettings "defaultssXsl"
     
      let xmlForBrowser = webBrowserXmlText defaultsXslFile
      
        
     
      