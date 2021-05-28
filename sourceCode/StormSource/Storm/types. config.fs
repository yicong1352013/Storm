#light

namespace Storm.Types.Configuration

   open System.Configuration
   open System.Xml
   open System.Xml.Serialization
   open System.Reflection
   open Storm.Utilities.GenHelper
   open System.IO
   //
   // These Config types can be modified by the user via the UI, hence
   //  the items are marked as mutable
   //
   
   type MiscConfigItems3 =
     {
       mutable ProxyServer : string 
       mutable ProxyUsername : string
       mutable ProxyPassword : string
       mutable ProxyDomain : string
       mutable WebServiceUsername : string
       mutable WebServicePassword : string
       mutable WebServiceDomain : string

       mutable WebServiceUseDefaultCredentials :bool
       mutable ProxyUseDefaultCredentials :bool
       mutable SoapCallTimeout : int
        //KeepAlive : bool
     
     }
   
   
   //This type has mutable members because the users are allowed
   // to modify these values from the UI.  This was also created as a class
   // instead of a Record type so that it can be serialized as XML
  // [<XmlRoot(Namespace="",IsNullable=false )>]
   type MiscConfigItems() =
     let mutable _proxyServer = ""
     let mutable _proxyUsername = ""
     let mutable _proxyPassword= ""
     let mutable _proxyDomain = ""
     let mutable _webServiceUsername =""
     let mutable _webServicePassword = ""
     let mutable _webServiceDomain = ""

     let mutable _webServiceUseDefaultCredentials = false
     let mutable _proxyUseDefaultCredentials = false
     let mutable _soapCallTimeout = 900 //milliseconds
     
     
     member x.ProxyServer 
        with get() = _proxyServer
        and set s = _proxyServer <- s
     
     member x.ProxyUsername 
        with get() = _proxyUsername
        and set s = _proxyUsername <- s
     
     member x.ProxyPassword 
        with get() = _proxyPassword
        and set s = _proxyPassword <- s       
 
     member x.ProxyDomain 
        with get() = _proxyDomain
        and set s = _proxyDomain <- s 
     
     member x.WebServiceUsername 
        with get() = _webServiceUsername
        and set s = _webServiceUsername <- s 
     member x.WebServicePassword 
        with get() = _webServicePassword
        and set s = _webServicePassword <- s 
     member x.WebServiceDomain 
        with get() = _webServiceDomain
        and set s = _webServiceDomain <- s        
     member x.WebServiceUseDefaultCredentials 
        with get() = _webServiceUseDefaultCredentials
        and set s = _webServiceUseDefaultCredentials <- s  
     member x.ProxyUseDefaultCredentials 
        with get() = _proxyUseDefaultCredentials
        and set s = _proxyUseDefaultCredentials <- s 
     member x.SoapCallTimeout 
        with get() = _soapCallTimeout
        and set s = _soapCallTimeout <- s 
     member x.Clone() =
        let clone = new MiscConfigItems()
        clone.ProxyDomain <- x.ProxyDomain
        clone.ProxyPassword <- x.ProxyPassword
        clone.ProxyServer <- x.ProxyServer
        clone.ProxyUseDefaultCredentials <- x.ProxyUseDefaultCredentials
        clone.ProxyUsername <- x.ProxyUsername
        clone.SoapCallTimeout <- x.SoapCallTimeout
        clone.WebServiceDomain <- x.WebServiceDomain
        clone.WebServicePassword <- x.WebServicePassword
        clone.WebServiceUseDefaultCredentials <- x.WebServiceUseDefaultCredentials
        clone.WebServiceUsername <- x.WebServiceUsername
        clone
       
  
   type WsConfigItems2 =
     {
        mutable URL : string
        mutable ContentType : string
        mutable Method : string
        mutable SoapAction : string
        mutable AllowAutoRedirect : bool
        mutable Pipelined : bool
        mutable PreAuthenticate : bool
        mutable SendChunked : bool        
        mutable KeepAlive : bool
        mutable UseCookieContainer : bool


     }
   type WsConfigItems() =
     let mutable _URL = ""
     let mutable _contentType = ""
     let mutable _method = ""
     let mutable _soapAction = ""
     let mutable _allowAutoRedirect = false
     let mutable _pipelined = false
     let mutable _preAuthenticate = false
     let mutable _sendChunked = false
     let mutable _keepAlive = false
     let mutable _useCookieContainer = false
     
     member x.URL 
       with get() = _URL
       and set s = _URL <- s
     member x.ContentType 
       with get() = _contentType
       and set s = _contentType <- s  
     
     member x.Method 
       with get() = _method
       and set s = _method <- s  
     member x.SoapAction 
       with get() = _soapAction
       and set s = _soapAction <- s  
     member x.AllowAutoRedirect 
       with get() = _allowAutoRedirect
       and set s = _allowAutoRedirect <- s  
     member x.Pipelined 
       with get() = _pipelined
       and set s = _pipelined <- s  
     member x.PreAuthenticate 
       with get() = _preAuthenticate
       and set s = _preAuthenticate <- s  
     member x.SendChunked 
       with get() = _sendChunked
       and set s = _sendChunked <- s  
     member x.KeepAlive 
       with get() = _keepAlive
       and set s = _keepAlive <- s  
     member x.UseCookieContainer 
       with get() = _useCookieContainer
       and set s = _useCookieContainer <- s  
     member x.Clone() =
       let clone  = new WsConfigItems()
       clone.AllowAutoRedirect <- x.AllowAutoRedirect
       clone.ContentType <- x.ContentType
       clone.KeepAlive <- x.KeepAlive
       clone.Method <- x.Method
       clone.Pipelined <- x.Pipelined
       clone.PreAuthenticate <- x.PreAuthenticate
       clone.SendChunked <- x.SendChunked
       clone.SoapAction <- x.SoapAction
       clone.URL <- x.URL
       clone.UseCookieContainer <- x.UseCookieContainer
       clone
     
   type UIConfig() =
      let mutable openMethodInTabs = true
      let mutable colorRawSoap = true
      let mutable showDetailedExc = true
      
      member x.OpenMethodsInTabs 
         with get() = openMethodInTabs
         and set s = openMethodInTabs <- s
      member x.ColorRawSoap 
         with get() = colorRawSoap
         and set s = colorRawSoap <- s
      member x.ShowDetailedException
         with get() = showDetailedExc
         and set s = showDetailedExc <- s
      member x.Save() =
         let str =  serializeObject<_> x
         let filename =  Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location), "Storm.settings")
         stringToFile str filename
      static member Read(settingFile:string) =
         let doc = new XmlDocument()
         doc.Load(settingFile)
         deserializeXml<UIConfig> doc
         
              
        
      
   module ConfigurationImpl =
   
     //TODO : Delete this before uploading to codeplex
     (*let defaultConfig =
       {
          ProxyServer = "";
          ProxyUsername = "";
          ProxyPassword = "";
          ProxyDomain = "";          
          WebServiceUsername = "";
          WebServicePassword = "";
          WebServiceDomain = "";
          ProxyUseDefaultCredentials = false;
          WebServiceUseDefaultCredentials = false; //Anonymous
          SoapCallTimeout = 100000  //milliseconds
        }
      *)
     
     let defaultUiConfig =
       let temp = new UIConfig()
       temp.ColorRawSoap <- true
       temp.OpenMethodsInTabs <- true
       temp.ShowDetailedException <- true
       temp
        
     let defaultConfig =
       let temp = new MiscConfigItems()
       temp.ProxyServer <- ""
       temp.ProxyUsername <- ""
       temp.ProxyPassword <- ""
       temp.ProxyDomain <- ""          
       temp.WebServiceUsername <- ""
       temp.WebServicePassword <- ""
       temp.WebServiceDomain <- ""
       temp.ProxyUseDefaultCredentials <- false
       temp.WebServiceUseDefaultCredentials <- false //Anonymous
       temp.SoapCallTimeout <- 100000  //milliseconds
        
       temp.Clone()
       
     let defaultWsConfig=
       let temp = new WsConfigItems()
       temp.AllowAutoRedirect <- true
       temp.Pipelined <- true
       temp.PreAuthenticate <- false
       temp.SendChunked <- false
       temp.KeepAlive <- true
       temp.UseCookieContainer <- true
       temp.Clone()
       
       (*{        
         URL = ""
         ContentType = ""
         Method = ""
         SoapAction = ""
         AllowAutoRedirect = true;
         Pipelined = true;
         PreAuthenticate = false;
         SendChunked = false;        
         KeepAlive = true;
         UseCookieContainer = true;

       }*)