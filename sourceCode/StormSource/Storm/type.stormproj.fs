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
   open System.Xml
   open System.Net
   open System.Reflection
   open System.Xml.Schema
   open System.Drawing 
   open System.IO
   open System.Text
   open System.Xml.Serialization
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigurationImpl
   
    //For the user to change
   type ColorScheme =
     {
        mutable ForeColor : Color
        mutable GradientHighColor : Color
        mutable GradientLowColor : Color
     }
   
   type ProjTestCase() =
      let mutable name = ""
      let mutable inputList = []
      let mutable notes =""
      let mutable expectedOutput = ("","")
      member x.Name = name
      member x.LoadFrom(mtc:MethodTestCase) =
 
        expectedOutput <- mtc.ExpectedOutput
        inputList <- [mtc.Input]
        x.Notes <- mtc.Notes
        name <- mtc.Name
      member x.Notes
        with get() = notes
        and set s = notes <- escXmlSpecialChars s
        
      member x.ExpectedOutput = expectedOutput
      member x.TestInputs = inputList
         
   type ProjNode() =
      let mutable name = ""
      let mutable cfg = defaultWsConfig
      
      let mutable testCaselist = new ResizeArray<_>()
      member x.Name = name
      member x.TestCasesList = ResizeArray.to_list testCaselist
      member x.Config = cfg
      member x.LoadFrom(wm:WebMethNode2) =
         name <- wm.Name
         cfg <- wm.WebServiceConfig.Clone()
         wm.AllTestCases 
         |> List.iter (fun tcName ->
              let mtcOpt = wm.FindTestCase tcName
              if mtcOpt <> None then
                 let ptc = new ProjTestCase()
                 ptc.LoadFrom( Option.get mtcOpt)
                 testCaselist.Add(ptc)
           )
    
   // 
   // Project Items are webservices added by the user
   // which may/may not contain Test cases
   type ProjWsItem(url:string, serviceName:string) =
      let webmethods = new ResizeArray<ProjNode>()
      let mutable cfg = defaultConfig
      
      member x.ServiceName = serviceName
      member x.Url = url
      member x.Config = cfg
      member x.AddNode(w:WebMethNode2) =
        cfg <- w.MiscConfig.Clone()
        let projNode = new ProjNode()
        projNode.LoadFrom(w)
        webmethods.Add(projNode)
      member x.AllWebMethods = ResizeArray.to_list webmethods
      //new() = new ProjWsItem("","")
  
   type StormProject() = 
     let projWsItem = new ResizeArray<ProjWsItem>()
     let mutable miscCfg :MiscConfigItems = defaultConfig
     let mutable wsCfg :WsConfigItems = defaultWsConfig
     let mutable colors = {ForeColor=Color.Black; GradientHighColor=Color.Black; GradientLowColor=Color.Black }  
     
     member x.AddWSProject(webSvc:IWebSvc) =
       let pItem = new ProjWsItem(webSvc.Url, webSvc.ServiceName)
       let allMethods = webSvc.GetAllWebserviceMethods()
       for methName in allMethods do
         let nodeOpt = webSvc.GetWebMethodNode methName
         if nodeOpt <> None then
           let node = Option.get nodeOpt
           if (List.length node.AllTestCases) > 0 then
              //let pItem = new ProjWsItem(webSvc.Url)
              pItem.AddNode(node) //Load as a projectItem
       
       projWsItem .Add(pItem)
       
     member x.WsProjectItems
       with get() =projWsItem //ResizeArray.to_list projWsItem 
     
     [<XmlIgnore>]
     member x.Colors
       with get() = colors
       and set (s) = colors <- s
     
     member x.Save(stormProjFile:string) =
       if String.IsNullOrEmpty(stormProjFile) then
         failwith "Project file cannot be empty!"
       
       //Structure is 
       // Root
       //   - Service1
       //        - data
       //   - Service2
       
       
       
       let sb = new StringBuilder()
       sb.Append( "<StormProject>" ) |> ignore
       
       let root = Path.GetDirectoryName(stormProjFile)
       if not (Directory.Exists(root)) then
          Directory.CreateDirectory(root) |> ignore
       else
          sb.Append(String.Format("<Root>{0}</Root>",root) ) |> ignore
          sb.Append("<Services>") |> ignore
          for item in x.WsProjectItems do
             let svcPath = Path.Combine(root, item.ServiceName)
             sb.Append(String.Format("<Service name=\"{0}\" >", item.ServiceName )) |> ignore
             sb.Append(String.Format("<Url>{0}</Url>", item.Url)) |> ignore
             sb.Append(String.Format("<Path>{0}</Path>", svcPath)) |> ignore
             
             createDirectory svcPath
             
             let cfgXml = serializeObject item.Config
             let cfgFile = Path.Combine(svcPath, String.Format("{0}_ws.config", item.ServiceName) )
             stringToFile cfgXml cfgFile
             
             sb.Append(String.Format("<Config>{0}</Config>", Path.GetFileName(cfgFile))) |> ignore
  
             sb.Append("<WebMethods>") |> ignore
  
             for webmethodNode in item.AllWebMethods do
               sb.Append(String.Format("<Method name=\"{0}\" >",  webmethodNode.Name)) |> ignore
  
               let nodePath = Path.Combine(svcPath, webmethodNode.Name)
               createDirectory nodePath
               
               let miscXml = serializeObject webmethodNode.Config
               let miscFile = Path.Combine(nodePath, String.Format("{0}_test.config", webmethodNode.Name) )
               
               sb.Append(String.Format("<Config>{0}</Config>",Path.GetFileName(miscFile))) |> ignore
  
               stringToFile miscXml miscFile
               for tc in webmethodNode.TestCasesList do
                 sb.Append(String.Format("<TestCase name=\"{0}\" >",tc.Name)) |> ignore
  
                 let tcPath = Path.Combine(nodePath, tc.Name)
                 createDirectory tcPath
                 
                 
                 tc.TestInputs  
                 |> List.iter (fun (label,content) ->
                   let tcInput = Path.Combine(tcPath, String.Format("{0}_testinput.xml", label))                   
                   stringToFile content tcInput
                   sb.Append(String.Format("<Input>{0}</Input>",Path.GetFileName(tcInput))) |> ignore
                   
                  )
                 
                 if (fst tc.ExpectedOutput) <> "" then
                    let tcOutput = Path.Combine(tcPath, String.Format("{0}_testoutput.xml", fst tc.ExpectedOutput ))                   
                    stringToFile (snd tc.ExpectedOutput) tcOutput
                    sb.Append(String.Format("<Output>{0}</Output>",Path.GetFileName(tcOutput))) |> ignore
                 
                 sb.Append(String.Format("<Notes>{0}</Notes>",tc.Notes)) |> ignore
                 sb.Append("</TestCase>") |> ignore 
                // 
               sb.Append("</Method>") |> ignore
             
             sb.Append("</WebMethods>") |> ignore
  
               //binSerializeObject (box item) datFile
             sb.Append("</Service>") |> ignore
             
          sb.Append("</Services>") |> ignore
       sb.Append( "</StormProject>" ) |> ignore
       createOrOverwriteFile (xmlPrettyPrint (sb.ToString()) Encoding.UTF8 ) stormProjFile 

        
     