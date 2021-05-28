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


namespace Storm.UI.Forms
   
   open System
   open System.Drawing
   open System.Drawing.Design
   open System.Windows.Forms
   open System.Text
   open System.Reflection
   open System.ComponentModel
   open System.IO
   open System.Xml
   open System.Collections.Generic
   open System.Xml.Serialization
   open System.Web.Services.Protocols 
   open System.Threading
   open Storm.Types
   open Storm.Types.WebSvcManagerOps
   open Storm.Types.WebHttp
   open Storm.Types.WebMethodNodeOps
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigReaderImpl
   open Storm.Types.Configuration.ConfigurationImpl
   open Storm.Types.LogManagerImpl
   open Storm.Types.CodeGenImpl
   open Storm.Types.WebSvcImpl
   open Storm.UI.Forms
   open Storm.Types.ReflectionOps
   open Storm.Utilities.LogHelper
   open Storm.Utilities.GenHelper
   open Storm.UI
   open Storm.CS.Lib.Soap
   open Storm.Types.Extensions.XmlNodeExt
   open Storm.Types.UICommon
   
   open TabControl
   //open Xsd2Xml
   
         
   type FFormMain() = 
     inherit FormMain()
     
     let myWebServices = new ResizeArray<IWebSvc>()
     let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
        
     member x.AllWebServices 
       with get() = myWebServices |> ResizeArray.map(fun w -> w.Url) |> ResizeArray.to_list
     member x.AddWebService(svc : IWebSvc) =
           myWebServices.Add(svc)  
     member x.GetWebService (url : string) =
        myWebServices |> ResizeArray.tryfind( fun p -> p.Url = url)
     member x.RemoveWebService(url:string) =
        let tempOpt = x.GetWebService(url)
        if tempOpt <> None then
           myWebServices.Remove( (Option.get tempOpt)) |> ignore
     member x.ClearWebServices() =
        myWebServices.Clear()
 
     //Implementation of ILog interface
     interface ILog with       
        member x.LogEvt = (logTrigger, logEvent)
  
   module FormMainOps =
      
      let private createFormMain =
        let head =  "Storm : powered by F#!"
        new FFormMain(Text = head) 
           
             
      //FORMS region
      let thisForm = createFormMain 
      let addwsForm = FormAddWsOps.thisForm
      let quickEditForm = FFormQuickEditOps.thisForm
      let formConfig = FFormConfigOps.thisForm
      let formTestDataSave = FFormTestDataSaveOps.thisForm
      // End of FORMS region
      
             
      let logger = LogManagerImpl.createLogManager thisForm.rtbLog
      let mutable currentWsAndMethod = ("", None) // String Uri, (MethodInfo option) pair
      let mutable globalMiscConfig : Option<MiscConfigItems> = None
      let mutable uiConfig = defaultUiConfig
      
      //let defaultsXslFile = readAppSettings "defaultssXsl"
      let activeTestEditReq = thisForm.tabcMainTests.SelectedItem
      
      //let xmlForBrowser = webBrowserXmlText defaultsXslFile
      let tvWebSvcTreeTag ty = {Type = ty; ObjectType = None; ObjectParentType = None; OriginalValue = None; ModifiedValue=None;  }
      let mutable currentStormProjectFile = ""
      
      let currentMethodNameAndInfo() = 
        let curMethodInfoOpt  =  snd currentWsAndMethod
        if curMethodInfoOpt <> None then
           let curMethodInfo = snd (Option.get curMethodInfoOpt)
           let curMethodName = fst (Option.get curMethodInfoOpt)
           curMethodName, Some(curMethodInfo)
        else
           "", None
           
      let curWebServiceOpt() = thisForm.GetWebService( fst currentWsAndMethod )
      let curMethodOpt() =      
        let curWebServiceOpt2 = curWebServiceOpt()
        if curWebServiceOpt2 <> None then
             let curWebService =Option.get curWebServiceOpt2
             let curmethodName, _ = currentMethodNameAndInfo()
             let webmethodOpt = curWebService.GetWebMethodNode(curmethodName)
             if webmethodOpt <> None then
               let webmethod = Option.get webmethodOpt
               Some(webmethod)
             else
               None
        else
           None
           
           
      //EVENTS region
      let _, addwsFormEvt = (addwsForm :> ILog).LogEvt   
      let logTrigger, thisFormEvt = (thisForm :> ILog).LogEvt   
      
      
      let writeInfoLog = logInfo >> logTrigger
      let writeErrLog = logErr >> logTrigger
      let writeSuccessLog = logSuccess >> logTrigger
      let writeWarningLog = logWarning >> logTrigger
      let writeExceptionLog (exc:#Exception) =
         match uiConfig.ShowDetailedException with
         | true -> writeErrLog (exc.ToString())
         | false -> writeErrLog exc.Message
         
      //START/STOP PRogressBar
      let progressBarStart state =
         match state with
         | true ->
            thisForm.tsProgressBar1.Style <- ProgressBarStyle.Marquee
            thisForm.tsLabelDoneOrProcessing.Text <- "Processing..."
            //thisForm.tsProgressBar1.Invalidate()
         | false ->       
            thisForm.tsProgressBar1.Style <- ProgressBarStyle.Blocks
            thisForm.tsProgressBar1.Step <- 0
           // thisForm.tsProgressBar1.Invalidate()
            thisForm.tsLabelDoneOrProcessing.Text <- "Done."
            
      addwsFormEvt.Add(fun s -> logger.Log(s))
      thisFormEvt.Add(fun s -> 
        if s.LogType = LogTypeEnum.Error then
           progressBarStart false
        logger.Log(s))
      //End of EVENTS region   
      
      let showNotifierPopup msg = 
         UICommon.showNotifier (thisForm.testEditorReq) msg
      
      let saveFile (rawContent:string) =
         match thisForm.saveFileDialog.ShowDialog() with
         | DialogResult.OK ->
             match thisForm.saveFileDialog.FileName with
             | "" -> "Filename cannot be empty" |> logErr |> logTrigger
             | filename -> 
                if File.Exists(filename) then
                  use strm = thisForm.saveFileDialog.OpenFile()
                  use sw = new StreamWriter(strm)
                  sw.Write(rawContent)
                  sw.Flush()
                  "Saved to " + filename |> writeInfoLog
                else
                  use sw = File.CreateText(filename)
                  sw.Write(rawContent)
                  sw.Flush()
                  "Created " + filename |> writeInfoLog
         | _ -> ()
      
      let setWsAndMethodInfoFromActiveTab(tabid:string) =
         if (tabid <> null) then
            let urlAndMethodArray = tabid.Split([| "_Storm_" |] , 2, StringSplitOptions.RemoveEmptyEntries )
            if urlAndMethodArray.Length =  2 then
                let ws = thisForm.GetWebService(urlAndMethodArray.[0]) |> Option.get
                let methInfoTup = ws.ReflectedWebMethods |> List.find(fun (name, meth) -> name = urlAndMethodArray.[1] )
                currentWsAndMethod <- ws.Url, Some(methInfoTup)
                true
            else
               false
          else 
             false
            
      let reqHead (data:HttpResponseData) = String.Format(" Status Code: {0}\n Content Length : {1}\n Content Type: {2}\n Server: {3}\n Status Description: {4}    ", [| box data.StatusCode; box data.ContentLength; box data.ContentType; box data.Server; box data.StatusDescription |])
               
      let setTestCaseRunnerResp (data:HttpResponseData) =
         let t: TestRunnerFS = thisForm.tabcMainTests.SelectedItem.Controls.Item(TestRunnerFSOps.runnerLabel) |> cast
         t.WriteInfoLog(reqHead data)
         t.SaveResponse(data.Response)
        
           
      let setTestEditor (te : TestEditor) (rawString:string option) (node:TreeNode option) =
         if rawString <> None then
            let str = rawString |> Option.get
           
            if File.Exists(defaultsXslFile) then
              if (stringToXmlDoc str) <> None then 
                te.RawText <- xmlPrettyPrint str Encoding.UTF8
                let content = xmlForBrowser str 
                te.XmlViewText <- content
            else
              logTrigger (logErr "defaultss.xsl is missing.  Please check the application config file.")
         if node <> None then
            let clone = (Option.get node).Clone()
            te.QuickEditNode <- unbox<TreeNode> clone
      
      let setSoapCallConfig (miscCfg:MiscConfigItems) (wsCfg:WsConfigItems) =
        thisForm.pgAppCOnfig.SelectedObject <- miscCfg.Clone()
        thisForm.pgWebServiceProp.SelectedObject <- wsCfg.Clone()
                         
      let processHttpReqData data =
         //thisForm.sxvRawRequest.ActualText <- Encoding.UTF8 |> xmlPrettyPrint data.Request
         if (thisForm.tabcMainTests.SelectedItem.TabType <> TabType.TestCaseTab) then
           let curwebMethodOpt = curMethodOpt()
           if curwebMethodOpt <> None then
              let webmethod = Option.get curwebMethodOpt
              webmethod.LastRunsParameters.SoapReq <- data.Request 
              try           
                //webmethod.LastRunsParameters.TvInputNode <- thisForm.testEditorReq.tvQuickEditInput.Nodes.Item(0)
                webmethod.LastRunsParameters.TvInputNode <- thisForm.ActiveTestEditorReq.tvQuickEditInput.Nodes.Item(0)
              with
              | _ -> () //BUG!!!
            
              if data.WebRequestConfig.Method.ToLower()  = "post" then
                 setSoapCallConfig webmethod.MiscConfig data.WebRequestConfig
                 webmethod.MiscConfig <- webmethod.MiscConfig
                 webmethod.WebServiceConfig <- data.WebRequestConfig
                     
           setTestEditor (thisForm.ActiveTestEditorReq) (Some(data.Request)) None
         else //testCasetab
           ()
         
            
      let processHttpRespData data =
        //bug ; if this is triggred by SendRaw req, the tree nodes and web browser
        // views are no longer synchronized. need to deserialize the soap req and resp
        // and rebuild the treeview.
         
         if (thisForm.tabcMainTests.SelectedItem.TabType <> TabType.TestCaseTab) then
          
           let curwebMethodOpt = curMethodOpt()
           if curwebMethodOpt <> None then
               let webmethod = Option.get curwebMethodOpt
             //webmethod.MiscConfig <- unbox<MiscConfigItems2> thisForm.pgAppCOnfig.SelectedObject
               webmethod.WebServiceConfig <- ( unbox<WsConfigItems> thisForm.pgWebServiceProp.SelectedObject).Clone()
               webmethod.LastRunsParameters.SoapResp <- data.Response
         
               if data.HttpReqMethod.ToLower() = "post" then
                // let reqHead = String.Format(" Status Code: {0}\n Content Length : {1}\n Content Type: {2}\n Server: {3}\n Status Description: {4}    ", [| box data.StatusCode; box data.ContentLength; box data.ContentType; box data.Server; box data.StatusDescription |])
                // thisForm.testEditorResponseControl1.RawRespHeader <-reqHead
                 thisForm.ActiveTestEditorResp.RawRespHeader <- reqHead data
                 webmethod.LastRunsParameters.RequestHead <- reqHead data
               
           setTestEditor (thisForm.ActiveTestEditorResp.testEditor1) (Some(data.Response)) None    
         else
           setTestCaseRunnerResp data
           
      let setDefaultSoapValues (ws:IWebSvc) webmethodName = 
        if (thisForm.tabcMainTests.SelectedItem.TabType <> TabType.TestCaseTab) then
           let webmethod = ws.GetWebMethodNode(webmethodName) |> Option.get
           
           let opt = ws.DefaultSoapMessages |> List.tryfind (fun (methodName, defSoap ) -> methodName = webmethod.Name )
           if opt <> None then
               let defSoap = opt |> Option.get |> snd
               webmethod.WebServiceConfig.ContentType <- defSoap.ContentType
               webmethod.WebServiceConfig.SoapAction <- defSoap.SoapAction
               webmethod.WebServiceConfig.Method <- "POST"
               setTestEditor (thisForm.ActiveTestEditorReq) (Some defSoap.SoapMsg ) None
               
      
      let findWsTreeNode key =
        //key = url
        //writeInfoLog ("Searching for node (key) :" + key )
        let node = thisForm.tvWebServices.Nodes.Find(key, true)
        node.[0]
      
      let findWebSvcMethodTreeNode url methodName =
        let wsNd = findWsTreeNode url
        let methNode = wsNd.Nodes.Find(methodName, true)
        methNode.[0]
        
      let worker (func: 'a -> 'b ) funcDone =
        let bg = new DelegatingBgWorker<_,_>(func)  
        bg.LogEvent.Add(fun m -> logTrigger (m)  )      
        bg.DoneEvent.Add(funcDone)
        bg.DoneEvent.Add(fun (args:RunWorkerCompletedEventArgs) -> 
          if args.Error <> null then
               writeExceptionLog args.Error 
           else
              let (_ :'b option),(elapsed:double) = unbox args.Result
              (formatElapsed elapsed) |> writeInfoLog
              progressBarStart false)
        
        fun funcArgs -> 
           bg.FunctionArguments <- funcArgs
           progressBarStart true
           bg.RunWorkerAsync() 
  
      //
      //  Builds the actual tree view to display the web services and its methods
      //     
      let buildWebServiceTreeNodes (ws:IWebSvc) =
         let root = thisForm.tvWebServices.Nodes.Item(0)
         
         //TreeNodeCollection.Find matches agains the treeNode.Name
         let found = root.Nodes.Find(ws.Url, true).Length > 0
         if found then
            root.Nodes.RemoveByKey(ws.Url)

         let topNode = new TreeNode(ws.Url, 6, 6)
         topNode.Name <-ws.Url
         topNode.Tag <- (tvWebSvcTreeTag TreeNodeType.WebService)
         
         let methodNodesArray = 
            let refMethods = ws.ReflectedWebMethods
            let temp = refMethods |> List.map (fun x ->
            
                let name,methInfo = x
                let trNode = new TreeNode(name,0,20) //imageIndex bullet, selectedImage = arrow
                trNode.Name <-name
                trNode.Tag <- (tvWebSvcTreeTag TreeNodeType.WebMethod)
                trNode
               )
            
            List.to_array(temp)
                  
         topNode.Nodes.AddRange(methodNodesArray)
         root.Nodes.Add(topNode) |> ignore
             
         thisForm.tvWebServices.ExpandAll()
         thisForm.tabcMainTests.Enabled <- true
         
         
   
      //
      //  Clear the form and remove the webservice model
      //  from memory.
      //   
      let clearThisForm() =
        thisForm.tvWebServices.Nodes.Item(0).Nodes.Clear()
        thisForm.testEditorReq.Clear()
        thisForm.testEditorResponseControl1.testEditor1.Clear()
        thisForm.ClearWebServices()
      
     
            
      let tvWebService_webService_AfterSelect_Event = 
        thisForm.tvWebServices.AfterSelect 
        |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
        |> Event.filter(fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag             
              nodeType.Type = TreeNodeType.WebService)  
                    
      tvWebService_webService_AfterSelect_Event.Add(fun args -> 
        currentWsAndMethod <- (args.Node.Name, None) 
        let temp = curWebServiceOpt()
        if temp<> None then
              thisForm.tvWebServices.ContextMenuStrip <- thisForm.ctxMenuStripWebService
              let str = (Option.get temp).Wsdl
              thisForm.wbWsdl.DocumentText <- xmlForBrowser str)     
         
      let tvWebService_webServiceOrNode_AfterSelect_Event = 
        thisForm.tvWebServices.AfterSelect 
        |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
        |> Event.filter(fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag                
              nodeType.Type = TreeNodeType.WebService || nodeType.Type = TreeNodeType.WebMethod)     
                                 
      let tvWebService_webMethodNode_AfterSelect_Event =      
         thisForm.tvWebServices.AfterSelect 
         |> Event.filter (fun args -> args.Node <> null && args.Node.Tag <> null)
         |> Event.filter (fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag          
              nodeType.Type = TreeNodeType.WebMethod         
              
              )
      
      let tvWebService_webServiceTestCaseRoot_AfterSelect_Event = 
        thisForm.tvWebServices.AfterSelect 
        |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
        |> Event.filter(fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag             
              nodeType.Type = TreeNodeType.TestCase)
      
      tvWebService_webServiceTestCaseRoot_AfterSelect_Event.Add(fun args -> 
          thisForm.tvWebServices.ContextMenuStrip <- thisForm.ctxMenuStripTestCase
          let selectedMethod = args.Node.Parent.Name                    
          let parentUrl = args.Node.Parent.Parent.Name
          let ws = thisForm.GetWebService(parentUrl) |> Option.get
          let curWebMethodTuple = ws.ReflectedWebMethods |> List.find (fun x -> (fst x) = selectedMethod ) 
          currentWsAndMethod <- (parentUrl, Some(curWebMethodTuple))
        )
       
      
      let tvWebService_webServiceTestCaseInput_AfterSelect_Event = 
        thisForm.tvWebServices.AfterSelect 
        |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
        |> Event.filter(fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag             
              nodeType.Type = TreeNodeType.TestInput)
              
      let tvWebService_NoCOntext_AfterSelect_Event = 
        thisForm.tvWebServices.AfterSelect 
        |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
        |> Event.filter(fun args -> 
              let nodeType = unbox<TreeNodeTag> args.Node.Tag             
              nodeType.Type <> TreeNodeType.TestCase && nodeType.Type <> TreeNodeType.WebService && nodeType.Type <> TreeNodeType.WebMethod)
      
      tvWebService_NoCOntext_AfterSelect_Event.Add(fun args -> thisForm.tvWebServices.ContextMenuStrip <- null )
      
      thisForm.tvWebServices.MouseDown.Add(fun me ->
           if MouseButtons.Right = me.Button then  //righ-click on the treenode
              let rcNode = thisForm.tvWebServices.GetNodeAt(me.Location)
              thisForm.tvWebServices.SelectedNode <- rcNode
           else
             ()
        )
      
//      tvWebService_webServiceTestCaseInput_AfterSelect_Event.Add(fun args ->
//          //thisForm.tvWebServices.ContextMenuStrip <- thisForm.ctxMenuStripTestCase
//          ()
//        )      
//        
                  
      let tvQuickEdit_AfterSelect_Event = 
         thisForm.testEditorReq.tvQuickEditInput.AfterSelect 
         |> Event.filter (fun args -> args.Node <> null && args.Node.Tag <> null && args.Node.Level > 1)
         //|> IEvent.filter (fun args -> args.Node.Nodes.Count = 0) // only the bottom nodes
            
      let quickEditAfterSelect (args:TreeViewEventArgs) =
            let objectVal = args.Node.Tag  
            let treeTag = unbox<TreeNodeTag> objectVal
          
            let wrapper = new QuickEditWrapper<_>(treeTag.OriginalValue |> Option.get)           
            let childTypes =
               if treeTag.ObjectParentType <> None && not ((treeTag.ObjectType |> Option.get).ToString().StartsWith("System")) then
                  let parentType = treeTag.ObjectParentType |> Option.get
                  let theType = treeTag.ObjectType |> Option.get
                  let initType =
                    match theType.IsAbstract with
                    | true -> [||]
                    | false -> [|theType|]
                    
                  let subTypes = 
                     theType.Assembly.GetTypes() 
                           |> Array.fold_left (fun state ty -> 
                                                  if ty.IsSubclassOf(theType) then
                                                    Array.append state [|ty|] 
                                                  else
                                                    state
                                               ) initType
                  
                  subTypes
               else
                  [||] 
                  
             
            if childTypes.Length > 1 then
              writeWarningLog "Polymorphic type(s) found!" 
              quickEditForm.InitSubTypes(childTypes)
            else
              quickEditForm.ShowExtraControls(treeTag.ObjectType |> Option.get)
              
            wrapper.Value <- treeTag.OriginalValue |> Option.get
            wrapper.IsNull <- treeTag.ModifiedValue = None
            
            
            do quickEditForm.pgInput.SelectedObject <-  wrapper 
            let posInTreeView  = args.Node.TreeView.PointToScreen(args.Node.Bounds.Location) 
            
            //Add a little offset so it doesnt cover the tree node
            do quickEditForm.Location <- new Point (posInTreeView.X + 40, posInTreeView.Y + 15)
            
            let dgResult = quickEditForm.ShowDialog()
            
            if dgResult = DialogResult.OK then
              let newWrapper : QuickEditWrapper<_> =  cast quickEditForm.pgInput.SelectedObject
              if newWrapper.IsNull then
                 args.Node.Tag <-  { Type =treeTag.Type;  ObjectType = treeTag.ObjectType; ObjectParentType = treeTag.ObjectParentType; OriginalValue = treeTag.OriginalValue; ModifiedValue = None}
                 args.Node.Text <- formatEq args.Node.Name (box "null")
                 if args.Node.Nodes.Count > 0 then
                    args.Node.Nodes.Clear()
              else
                 //if it was null previously then we need to rebuild the treeview
               
                 args.Node.Tag <- {Type =treeTag.Type;  ObjectType = treeTag.ObjectType ; ObjectParentType = treeTag.ObjectParentType; OriginalValue =Some(newWrapper.Value); ModifiedValue = Some(newWrapper.Value)}
                 args.Node.Text <- formatEq args.Node.Name newWrapper.Value
                // if treeTag.ModifiedValue = None then
                 args.Node.Nodes.Clear()
                 let tr = ReflectionOps.readType newWrapper.Value args.Node args.Node.Name
                 args.Node.Nodes.AddRange( [| for n in tr.Nodes -> n |])
                       
                      
      tvQuickEdit_AfterSelect_Event.Add(quickEditAfterSelect)   
      
      //
      //Builds the treeView of the output of the soap call
      //
      let build_tvOutput proxy (result:obj)= 
        let curMethodName, curMethodInfo = currentMethodNameAndInfo()
        if curMethodName <> "" && curMethodInfo <> None then
           let soapHead = WebMethodNodeOps.populateSoapHeader proxy SoapHeaderDirection.Out  (Option.get curMethodInfo) 
           let root = new TreeNode(curMethodName)
           root.Tag <- TreeNodeType.Root
           root.ImageIndex <- -1
           root.SelectedImageIndex <- -1   
           let headNode = new TreeNode("Header")
           headNode.Tag <- TreeNodeType.SoapHeader
           headNode.ImageIndex <- -1
           headNode.SelectedImageIndex <- -1 
           let headNodeChildren = 
                  soapHead 
                    |> List.map (fun x -> 
                          let name,objType,objValue = x                  
                          let temp2 = createObjectNode objType objValue name
                          temp2.ReadType(ReflectionOps.readType)
                          temp2.OutputTree
                        
                        )
                    |> List.to_array
        
           headNode.Nodes.AddRange(headNodeChildren)
        
           let bodyNode = new TreeNode("Body")
           bodyNode.Tag <- TreeNodeType.SoapBody
           bodyNode.ImageIndex <- -1
           bodyNode.SelectedImageIndex <- -1 
           if result <> null then
              let nodeName = String.Format("{0} result", (result.GetType().Name)  )
              let resultNode = new TreeNode(nodeName)
           
              let bodyNodeChild = readType result resultNode nodeName
              bodyNode.Nodes.Add(bodyNodeChild) |> ignore
                
           root.Nodes.AddRange([|headNode;bodyNode |])
           //thisForm.testEditorResponseControl1.QuickEditNode <- root
           thisForm.ActiveTestEditorResp.QuickEditNode <- root
           
        let curwebMethodOpt = curMethodOpt()
        if curwebMethodOpt <> None then
             let webmethod = Option.get curwebMethodOpt           
             //webmethod.LastRunsParameters.TvOutputNode <- thisForm.testEditorResponseControl1.testEditor1.tvQuickEditInput.Nodes.Item(0)
             webmethod.LastRunsParameters.TvOutputNode <- thisForm.ActiveTestEditorResp.testEditor1.tvQuickEditInput.Nodes.Item(0)
      
             
      let sendRawRequestClickHandler (args:EventArgs) =     
     
           //let rawRequest = thisForm.testEditorReq.RawText.Replace("\n","")
           let rawRequest = thisForm.ActiveTestEditorReq.RawText.Replace("\n","")
           setWsAndMethodInfoFromActiveTab(thisForm.tabcMainTests.SelectedItem.ID) |> ignore
           
           if String.IsNullOrEmpty(rawRequest) then
             "Cannot send an empty request" |> logErr  |> logTrigger
           else
             
             let curMethodNode  = curMethodOpt() |> Option.get 
             let curMiscCfg, curWsCfg  = curMethodNode.MiscConfig, curMethodNode.WebServiceConfig
             let curProxy = (curWebServiceOpt() |> Option.get).ConfiguredClientProxy(Some(curMiscCfg))
             let workerDone (args:RunWorkerCompletedEventArgs) =   
                 //thisForm.testEditorReq.ShowRawEdit()            
                 if args.Error <> null then
                    logTrigger (logErr (args.Error.ToString()))
                 else
                   logTrigger (logSuccess "Done.")
                   
             if  thisForm.ActiveTestEditorResp.CurrentView = ViewType.Tree   then
                    thisForm.ActiveTestEditorResp.ShowXmlView()
                    
             let invokeWs = MethodInvokerOps.invokeViaHttp rawRequest curMiscCfg curWsCfg
             let invokeWorker = worker invokeWs workerDone
             writeInfoLog "Invoking..."
             invokeWorker curProxy
             
      //*********************************************    
      //Submits the data in tvINput to the webservice via Reflection
      //*********************************************
      let quickEditClickHandler (args:EventArgs) =
        //Pass the "Body" node of the treeView
       // if  thisForm.testEditorReq.tvQuickEditInput.Nodes.Count > 0 then
        if  thisForm.ActiveTestEditorReq.tvQuickEditInput.Nodes.Count > 0 then
          
          setWsAndMethodInfoFromActiveTab(thisForm.tabcMainTests.SelectedItem.ID) |> ignore
          //Body node
          (*let bodyNode = thisForm.testEditorReq.tvQuickEditInput.Nodes.Item(0).Nodes.Item(1)
          let headNode = thisForm.testEditorReq.tvQuickEditInput.Nodes.Item(0).Nodes.Item(0)*)
          let bodyNode = thisForm.ActiveTestEditorReq.tvQuickEditInput.Nodes.Item(0).Nodes.Item(1)
          let headNode = thisForm.ActiveTestEditorReq.tvQuickEditInput.Nodes.Item(0).Nodes.Item(0)
          let methodParams = readNode (bodyNode)
                   
          let headName = 
            match headNode.Nodes.Count with
            | c when c > 0 -> headNode.Nodes.Item(0).Name
            | _ -> ""
          
          let curWebServiceOpt = thisForm.GetWebService( fst currentWsAndMethod )
          if curWebServiceOpt <> None then
            let curMethodInfoOpt  =  snd currentWsAndMethod
            let curWebService = Option.get curWebServiceOpt
            
        
            if curMethodInfoOpt <> None then
               let curMethodInfo = snd (Option.get curMethodInfoOpt)
               let curMethodName = fst (Option.get curMethodInfoOpt)
               let miscCfg = (curWebService.GetWebMethodNode(curMethodName) |> Option.get).MiscConfig
               
               let proxy = curWebService.ConfiguredClientProxy(Some(miscCfg))
               
                  
               
       
               if not (String.IsNullOrEmpty(headName)) then
                 let tag = unbox<TreeNodeTag> (headNode.Nodes.Item(0).Tag)
                 if tag.ModifiedValue <> None then
                   let headerParams = readNode (headNode)
                   setSoapHead proxy curMethodInfo headName (headerParams.GetValue([|0L|]))
                            
               let workerDone (args:RunWorkerCompletedEventArgs) =               
                 progressBarStart false
                 if args.Error <> null then
                    ()
                 else
                   logTrigger (logSuccess "Done.")  
                   let (res: obj option), (elapsed:double) = unbox args.Result
                   
                   if res <> None then
                      let objRes = Option.get res
                      build_tvOutput proxy objRes
                   
               let invokeFunc = MethodInvokerOps.invokeViaReflection proxy curMethodInfo
               let invokeWorker = worker invokeFunc workerDone
               writeInfoLog "Invoking..."
               invokeWorker methodParams
           
            else
               writeErrLog "No method selected."
        else
          writeErrLog "No method selected."
      

      
      //let invHandler = new EventHandler(quickEditClickHandler)
      //let sendRawHandler = new EventHandler(sendRawRequestClickHandler )
      
      //
      // Default sending of soap request is by reflection
      //     
      let btnGoClick args =
        match  thisForm.ActiveTestEditorReq.CurrentView with
        | ViewType.Tree -> quickEditClickHandler args
        | _ -> sendRawRequestClickHandler args
      
      thisForm.testEditorReq.btnGo.Click.Add(btnGoClick)
      
      
      //Populate the input tree view in QuickEdit tab        
      let buildTreeViewInput (curWebMethodTuple: string * MethodInfo) (thisWebService:IWebSvc)=
        
          let thisMethodInfo = snd curWebMethodTuple
          let methodName = fst curWebMethodTuple
          let webmethodNode = new WebMethNode2( methodName )
          thisWebService.AddWebMethodNode webmethodNode
          currentWsAndMethod <- (thisWebService.Url, Some(curWebMethodTuple))
        
          let soapHead = WebMethodNodeOps.populateSoapHeader (thisWebService.ConfiguredClientProxy(None)) SoapHeaderDirection.In thisMethodInfo
          let soapBody = WebMethodNodeOps.populateSoapBody thisMethodInfo
        
        
          //Bind the header
          let root = new TreeNode(methodName)
          root.Tag <- TreeNodeType.Root
        
          let headNode = new TreeNode("Header")
          headNode.Tag <- TreeNodeType.SoapHeader
          let headNodeChildren = 
            soapHead 
              |> List.map (fun x -> 
                    let name,objType,objValue = x                  
                    let temp2 = createObjectNode objType objValue name
                    temp2.ReadType(ReflectionOps.readType)
                    temp2.OutputTree
                   // let temp = new TreeNode( (fst x) + " = ")
                   //temp
                 )
               |> List.to_array
        
          headNode.Nodes.AddRange(headNodeChildren)
        
          let bodyNode = new TreeNode("Body")
          bodyNode.Tag <- TreeNodeType.SoapBody
          let bodyNodeChildren = 
            soapBody 
              |> List.map (fun x -> 
                    let name,objType,objValue = x                  
                    let temp2 = createObjectNode objType objValue name
                    temp2.ReadType(ReflectionOps.readType)
                    temp2.OutputTree

                 )
               |> List.to_array
        
          bodyNode.Nodes.AddRange(bodyNodeChildren)
        
          root.Nodes.Add(headNode) |> ignore
          root.Nodes.Add(bodyNode) |> ignore
        
      
          root
          (*setTestEditor (thisForm.testEditorReq) (Some("")) (Some(root))
          setTestEditor (thisForm.testEditorReq) (Some("")) None*)
          //setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) (Some(root))
          //setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) None
      
      let buildTreeInput2 (url:string) (methodname:string) =
        let thisWebServiceOpt = thisForm.GetWebService(url)
        
        if thisWebServiceOpt <> None then
             let thisWebService = Option.get thisWebServiceOpt
             let curWebMethodTuple = thisWebService.ReflectedWebMethods |> List.find (fun x -> (fst x) = methodname )             
             let root = buildTreeViewInput curWebMethodTuple thisWebService
             
             setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) (Some(root))
          //   setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) None 
             
      let buildTreeInput (args:TreeViewEventArgs) =
          let url = args.Node.Parent.Name
          let methodname = args.Node.Name
          buildTreeInput2 url methodname
          
//          let thisWebServiceOpt = thisForm.GetWebService(url)
//        
//          if thisWebServiceOpt <> None then
//             let thisWebService = Option.get thisWebServiceOpt
//             let curWebMethodTuple = thisWebService.ReflectedWebMethods |> List.find (fun x -> (fst x) = args.Node.Name )             
//             let root = buildTreeViewInput curWebMethodTuple thisWebService
//             
//             setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) (Some(root))
//             setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) None 
//      //
      // WebService Node After Select Event
      //  
      tvWebService_webMethodNode_AfterSelect_Event.Add(fun args ->
            
            thisForm.tvWebServices.ContextMenuStrip <- thisForm.ctxMenuStripWebMethod
          
            //thisForm.tabcMainTest.Add
            progressBarStart true
          (*  thisForm.testEditorResponseControl1.testEditor1.Clear()
            thisForm.testEditorReq.Clear()*)
            
            let activeTestEditorResp =
                if thisForm.ActiveTestEditorResp <> null then
                     thisForm.ActiveTestEditorResp
                 else
                     thisForm.testEditorResponseControl1  //QuickEdit
            let activeTestEditorReq =
                if thisForm.ActiveTestEditorReq <> null then
                     thisForm.ActiveTestEditorReq
                 else
                     thisForm.testEditorReq     //QuickEdit

            let wsUrl = args.Node.Parent.Name
            let curWsOpt = thisForm.GetWebService(wsUrl)
            if curWsOpt <> None then
              let ws = Option.get curWsOpt
              let curWsMethodOpt = ws.GetWebMethodNode(args.Node.Name)
                          
              if curWsMethodOpt <> None then
                let curWebMethodTuple = ws.ReflectedWebMethods |> List.find (fun x -> (fst x) = args.Node.Name ) 
                currentWsAndMethod <- (wsUrl, Some(curWebMethodTuple) )
                let webmethod = Option.get curWsMethodOpt
                setDefaultSoapValues ws webmethod.Name
                
                let id = ws.GenerateMethodId(webmethod.Name)
                let tabcItem = thisForm.FindTabControlByID(id)
                                   
                        
                let ranPreviously = webmethod.LastRunsParameters.SoapReq <> "" && webmethod.LastRunsParameters.SoapResp <> ""
                
                if (thisForm.tabcMainTests.SelectedItem = thisForm.tabcQuickTest ) then
                   
                   activeTestEditorResp.testEditor1.Clear()
                   activeTestEditorReq.Clear()    
                   
                   setDefaultSoapValues ws webmethod.Name 
                   //webmethod.WebServiceConfig.URL <- ws.Url
                   setSoapCallConfig webmethod.MiscConfig webmethod.WebServiceConfig
                   
                   if ranPreviously then
                     activeTestEditorResp.RawRespHeader <- webmethod.LastRunsParameters.RequestHead
                     setTestEditor (activeTestEditorReq)  (Some(webmethod.LastRunsParameters.SoapReq)) (Some(webmethod.LastRunsParameters.TvInputNode))
                     setTestEditor (activeTestEditorResp.testEditor1)  (Some(webmethod.LastRunsParameters.SoapResp)) (Some(webmethod.LastRunsParameters.TvOutputNode))
                   else
                      buildTreeInput args
                      setSoapCallConfig webmethod.MiscConfig webmethod.WebServiceConfig
                      
                      
                elif webmethod.Name = thisForm.tabcMainTests.SelectedItem.Title then 
                    if ranPreviously then
                       activeTestEditorResp.RawRespHeader <- webmethod.LastRunsParameters.RequestHead
                       setTestEditor (activeTestEditorReq)  (Some(webmethod.LastRunsParameters.SoapReq)) (Some(webmethod.LastRunsParameters.TvInputNode))
                       setTestEditor (activeTestEditorResp.testEditor1)  (Some(webmethod.LastRunsParameters.SoapResp)) (Some(webmethod.LastRunsParameters.TvOutputNode))
               
                else
                  ()
              else
                //Build the treeview for the first time
                buildTreeInput args
                //setSoapCallConfig webmethod.MiscConfig webmethod.WebServiceConfig
                
            progressBarStart false  
              //enableDisableTsTestData()
        )
    
     
      
      
      //
      // tsBtColor Scheme
      //       
      thisForm.tsBtnColorScheme.Click.Add(fun _ ->  ()
      
//      thisForm.tabConfigs.SelectedTab <- thisForm.tabPageColorSchem)                   
//      thisForm.pgColorScheme.PropertyValueChanged.Add(fun args ->
//              let label =args.ChangedItem.Label
//              let colr :Color = cast (args.ChangedItem.Value)
//              if label = "GradientLowColor" then
//                thisForm.gradientClock.GradientLowColor <- colr
//                thisForm.gradientCaptionWebCfg.GradientLowColor <- colr
//                thisForm.gradientLine1.GradientLowColor <- colr
//                thisForm.testEditorReq.gpToolBar.GradientLowColor <- colr
//                thisForm.testEditorResponseControl1.testEditor1.gpToolBar.GradientLowColor <- colr
//                formTestDataSave.gradientPanel1.GradientLowColor <- colr
//                formConfig.gradientPanel1.GradientLowColor <- colr
//              elif label = "GradientHighColor" then
//                thisForm.gradientClock.GradientHighColor <- colr
//                thisForm.gradientCaptionWebCfg.GradientHighColor <- colr
//                thisForm.gradientLine1.GradientHighColor <- colr
//                thisForm.testEditorReq.gpToolBar.GradientHighColor <- colr
//                thisForm.testEditorResponseControl1.testEditor1.gpToolBar.GradientHighColor <- colr
//                formTestDataSave.gradientPanel1.GradientHighColor <- colr
//                formConfig.gradientPanel1.GradientHighColor <- colr
//              else
//                thisForm.gradientClock.ForeColor <- colr
//                thisForm.gradientCaptionWebCfg.ForeColor <- colr
//                thisForm.testEditorReq.gpToolBar.ForeColor <- colr
//                thisForm.testEditorResponseControl1.testEditor1.gpToolBar.ForeColor <- colr
//                formTestDataSave.gradientPanel1.ForeColor <- colr
//                formConfig.gradientPanel1.ForeColor <- colr
        )
      
      let showFormConfig() =
//        let curScheme: ColorScheme = cast thisForm.pgColorScheme.SelectedObject
//        formConfig.gradientPanel1.GradientHighColor <- curScheme.GradientHighColor
//        formConfig.gradientPanel1.GradientLowColor <- curScheme.GradientLowColor
//        formConfig.gradientPanel1.ForeColor <- curScheme.ForeColor
            
        if formConfig.ShowDialog() = DialogResult.OK then
           let curwsOpt = curWebServiceOpt()
           if curwsOpt <> None then
             let curws = Option.get curwsOpt
             let methodsList =curws.GetAllWebserviceMethods()
             for n in methodsList do
               let temp = curws.GetWebMethodNode n
               if temp <> None then
                 let node = Option.get temp
                 node.MiscConfig <- formConfig.GlobalConfig
           globalMiscConfig <- Some (formConfig.GlobalConfig )
           uiConfig <- formConfig.UserInterfaceConfig
           thisForm.testEditorReq.ColorRawSoap <- uiConfig.ColorRawSoap
           thisForm.testEditorResponseControl1.testEditor1.ColorRawSoap <- uiConfig.ColorRawSoap
           
        else
           ()
       
      thisForm.pgAppCOnfig.PropertyValueChanged.Add(fun _ -> 
         let curwebMethodOpt = curMethodOpt()
         if curwebMethodOpt <> None then
            let webmethod = Option.get curwebMethodOpt
            webmethod.MiscConfig <- unbox<MiscConfigItems> thisForm.pgAppCOnfig.SelectedObject
       )
      thisForm.pgWebServiceProp.PropertyValueChanged.Add(fun _ -> 
         let curwebMethodOpt = curMethodOpt()
         if curwebMethodOpt <> None then
            let webmethod = Option.get curwebMethodOpt
            webmethod.WebServiceConfig <- unbox<WsConfigItems> thisForm.pgWebServiceProp.SelectedObject
       )
            
            
      //
      // BtnConfig Click
      //
      thisForm.tsBtnConfig.Click.Add(fun _ -> showFormConfig())

      thisForm.clocktimer1.Tick.Add(fun args -> 
         thisForm.gradientClock.Text <- String.Format ("{0},{1}", DateTime.Now.ToLocalTime().ToString(), DateTime.Today.DayOfWeek.ToString() ))
      
      
      let notYetImplemented _ = writeErrLog "Not yet implemented"
      thisForm.tsMenuItemPlugin.Click.Add(notYetImplemented)
      
      let setUpTheme() =
      
         let renderMode = 
           match theme.RenderMode.ToLower() with
           | "glass" -> Ascend.Windows.Forms.RenderMode.Glass
           | "flat" ->Ascend.Windows.Forms.RenderMode.Flat
           | _ -> Ascend.Windows.Forms.RenderMode.Gradient
           
         thisForm.gradientClock.GradientHighColor <- theme.GradientHighColor
         thisForm.gradientClock.GradientLowColor <- theme.GradientLowColor
         thisForm.gradientClock.RenderMode <- renderMode
         
         thisForm.gradientCaptionWebCfg.GradientHighColor <- theme.GradientHighColor
         thisForm.gradientCaptionWebCfg.GradientLowColor <- theme.GradientLowColor
         thisForm.gradientCaptionWebCfg.RenderMode <- renderMode
         
         thisForm.testEditorReq.gpToolBar.GradientHighColor <- theme.GradientHighColor
         thisForm.testEditorReq.gpToolBar.GradientLowColor <- theme.GradientLowColor
         thisForm.testEditorReq.gpToolBar.RenderMode <- renderMode
         
         thisForm.testEditorResponseControl1.testEditor1.gpToolBar.GradientHighColor <- theme.GradientHighColor
         thisForm.testEditorResponseControl1.testEditor1.gpToolBar.GradientLowColor <- theme.GradientLowColor
         thisForm.testEditorResponseControl1.testEditor1.gpToolBar.RenderMode <- renderMode

         formConfig.gradientPanel1.GradientHighColor <- theme.GradientHighColor
         formConfig.gradientPanel1.GradientLowColor <- theme.GradientLowColor
         formConfig.gradientPanel1.RenderMode <- renderMode
         
         addwsForm.gradientPanel1.GradientHighColor <- theme.GradientHighColor
         addwsForm.gradientPanel1.GradientLowColor <- theme.GradientLowColor
         addwsForm.gradientPanel1.RenderMode <- renderMode
         
         quickEditForm.gradientPanel1.GradientHighColor <- theme.GradientHighColor
         quickEditForm.gradientPanel1.GradientLowColor <- theme.GradientLowColor
         quickEditForm.gradientPanel1.RenderMode <- renderMode
         
         formTestDataSave.gradientPanel1.GradientHighColor <- theme.GradientHighColor
         formTestDataSave.gradientPanel1.GradientLowColor <- theme.GradientLowColor
         formTestDataSave.gradientPanel1.RenderMode <- renderMode
         
         

         //theme.GradientHighColor
         
         ()
      
  
      ///Form_Load event
      thisForm.Load.Add(fun _ -> 
      
          //splash screen
          let splash = new Splash()
          splash.ShowInTaskbar <- false
          let dg = splash.ShowDialog(thisForm) 
//          let curScheme = 
//           
//            { 
//               ForeColor = thisForm.gradientClock.ForeColor;
//               GradientHighColor = thisForm.gradientClock.GradientHighColor;
//               GradientLowColor = thisForm.gradientClock.GradientLowColor
//            }    
//            
          setUpTheme()
          //initialize the UI config
          FFormConfigOps.load (uiConfig.ConfigFile)
          uiConfig <- formConfig.UserInterfaceConfig
         
          //thisForm.pgColorScheme.SelectedObject <- curScheme
          thisForm.tabcMainTests.Enabled <- false
          thisForm.testEditorReq.ShowQuickEdit();
          thisForm.testEditorResponseControl1.testEditor1.ShowQuickEdit();
                    
          globalMiscConfig <- Some(defaultConfig)
         // enableDisableTsTestData()
          
          //check discoclient
          let creator =  
            let temp = StormReqCreateOps.createCustomHttpListener
            temp.RequestEvent.Add(fun data -> if data.Request <> "" then processHttpReqData data) 
            temp.ResponseEvent.Add(fun data -> if data.Response <> "" then processHttpRespData data)
            temp
            
          //Register the DelegattingHttpReq class as the handler for "http"
          StormReqCreateOps.registerPrefix(creator)
          
           
      )
      
      
      
      thisForm.helpToolStripButton.Click.Add(fun args -> 
          let temp = new AboutStorm()
          temp.ShowDialog(thisForm) |> ignore
        )
      
      let btnXmlView args =
         //let displayText = xmlForBrowser thisForm.testEditorReq.RawText
         //thisForm.testEditorReq.XmlViewText <- displayText
         //thisForm.testEditorReq.ShowXmlView()
         let displayText = xmlForBrowser thisForm.ActiveTestEditorReq.RawText
         thisForm.ActiveTestEditorReq.XmlViewText <- displayText
         thisForm.ActiveTestEditorReq.ShowXmlView()
         
      thisForm.testEditorReq.btnXmlView.Click.Add(btnXmlView)
      
      
      thisForm.testEditorResponseControl1.testEditor1.btnEditRaw.Click.Add (fun _  -> 
        thisForm.testEditorResponseControl1.testEditor1.rtbRaw.ReadOnly <-true // the soapResponse is not editable
        )
        
      
      let mergeWithWSMethodNode (nodeIntvWebService:TreeNode) (methodNode:WebMethNode2) (testCase:MethodTestCase)=
         methodNode.AddOrUpdateTestCase(testCase)
         let treeOpt = methodNode.RenderTestCaseView(testCase.Name) 
         if treeOpt <> None then
            let tree = Option.get treeOpt
                      
            let tcn = nodeIntvWebService.Nodes.Find(testCase.Name, true)
            if (tcn <> null && tcn.Length > 0) then 
               //UPDATE if existing
               tcn.[0].Nodes.Clear()
               for n in tree.Nodes do tcn.[0].Nodes.Add(n) |> ignore
            else   
               nodeIntvWebService.Nodes.Add(tree) |> ignore
         else
           ()
      
        
      let btnSaveRawSoap isReq =
        (* let selectedTvWsNode = thisForm.tvWebServices.SelectedNode
         let tag = unbox<TreeNodeTag> selectedTvWsNode.Tag
         *)
         
         let isValidReqView = thisForm.ActiveTestEditorReq.CurrentView <> ViewType.Tree && isReq
         let isValidRespView = thisForm.ActiveTestEditorResp.testEditor1.CurrentView <> ViewType.Tree && (not isReq)
      
         if  isValidRespView || isValidReqView  then // && (tag.Type = TreeNodeType.WebMethod) then
            if curWebServiceOpt() <> None then
              let curWs = Option.get (curWebServiceOpt())
              //add webNode
              let methods = curWs.GetAllWebserviceMethods()
              let methodNode = curMethodOpt() |> Option.get   
              let nodeIntvWebService =   findWebSvcMethodTreeNode curWs.Url methodNode.Name
            
            
            //TODO : list current test cases
              let curTestCases:string list =
                 if nodeIntvWebService.Nodes.Count <= 0 then
                   []
                 else
                   let temp2 = new ResizeArray<string>()
                   for n in nodeIntvWebService.Nodes do
                     let ty = (unbox<TreeNodeTag> n.Tag).Type 
                     if ty = TreeNodeType.TestCase then
                        temp2.Add(n.Name)
                   ResizeArray.to_list temp2
                 
              formTestDataSave.TestCaseList <- methodNode.AllTestCases
              if isReq then
                 formTestDataSave.RawContentToSave <- thisForm.ActiveTestEditorReq.RawText
                 formTestDataSave.Mode <- SaveMode.TestCaseInput
                 
              else
                 formTestDataSave.RawContentToSave <- thisForm.ActiveTestEditorResp.RawText 
                 formTestDataSave.Mode <- SaveMode.TestCaseExpectedOutput
                   
             
              if formTestDataSave.ShowDialog() = DialogResult.OK then         
                if formTestDataSave.TestCaseLabel <> "" && formTestDataSave.TestInputLabel <> "" then 
                 //
                 //save as project test data    
                 //
                   
                   let testCase = 
                      let tcOpt = methodNode.FindTestCase(formTestDataSave.TestCaseLabel)                   
                      if tcOpt <> None then
                         tcOpt |> Option.get
                      else
                         new MethodTestCase(formTestDataSave.TestCaseLabel, TestType.Good,methodNode.Name )
                         
                   testCase.Notes <- formTestDataSave.TestCaseNote
                   if isReq then
                      testCase.AddInput( formTestDataSave.TestInputLabel,  formTestDataSave.RawContentToSave)
                   else
                      testCase.AddExpectedOutput( formTestDataSave.TestInputLabel,  formTestDataSave.RawContentToSave)
                   // ** Tree structure **
                   // url
                   //  -webmethod
                   //    - testcase
                   //          - input
                   //    - note  
                   mergeWithWSMethodNode  nodeIntvWebService methodNode  testCase     
                   
                   if isReq then
                      showNotifierPopup "Don't forget to also save the expected response"
                   if not nodeIntvWebService.IsExpanded then nodeIntvWebService.Toggle() 
                else
                   writeErrLog "Test case name/label not provided"
              else
               ()
         elif (thisForm.ActiveTestEditorReq.RawText.Replace("\n","") <> "") then
           //Normal file save
           saveFile  thisForm.ActiveTestEditorReq.RawText
         else
           writeInfoLog "No soap request to save. Please invoke the method first."
              
       
        
      thisForm.testEditorReq.btnSave.Click.Add(fun _ -> btnSaveRawSoap true)        
      
      //
      // tsBtnSaveRawResp Event
      //

      let btnSaveClickResp args =
         saveFile thisForm.ActiveTestEditorResp.RawText 
         
      thisForm.testEditorResponseControl1.testEditor1.btnSave.Click.Add(fun _ -> btnSaveRawSoap false)     
      
      let projInstance (filename:string) (ws:IWebSvc)=
         let temp = binDeserializeObject<StormProject>  filename ws.Assembly
         temp
                 
             
      let loadTestCasesOfWs (curws:IWebSvc) =
         let stormProjDoc = new XmlDocument()
         stormProjDoc.Load(currentStormProjectFile)
         
         let xp = String.Format("/StormProject/Services/Service[Url='{0}']",curws.Url)
         let svcXmlNode = stormProjDoc.SelectSingleNode(xp)
         
         let svcPath = svcXmlNode.SelectSingleNode("Path").InnerText
         let miscCfgFile = Path.Combine(svcPath, svcXmlNode.SelectSingleNode("Config").InnerText)
         
         let nodesettingsMisc =
            let docOpt, err = loadXmlFile miscCfgFile
            if docOpt <> None then
               let doc = docOpt |> Option.get
              // let xmlnsManager = new XmlNamespaceManager(doc.NameTable)
              // xmlnsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")
              // xmlnsManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema")

               (deserializeXml<MiscConfigItems> doc) |> Option.get
            else
              writeWarningLog err
              writeWarningLog "using default settings..."
              defaultConfig
         
         
         let methodNodesList = [for n in svcXmlNode.SelectNodes("WebMethods/Method") -> n]
         
         methodNodesList |> List.iter (fun methXmlNode ->
              let name = methXmlNode.SelectSingleNode("@name").InnerText
              let nodePath = Path.Combine(svcPath, name)
              let soapReqCfgFile = Path.Combine(nodePath, methXmlNode.SelectSingleNode("Config").InnerText)
              let soapReqSettings =
                let docOpt, err = loadXmlFile soapReqCfgFile
                if docOpt <> None then
                  let doc = docOpt |> Option.get
                  //let xmlnsManager = new XmlNamespaceManager(doc.NameTable)
                  //xmlnsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")
                  //xmlnsManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema")

                  (deserializeXml<WsConfigItems> doc) |> Option.get
                else
                  writeWarningLog err
                  writeWarningLog "using default settings..."
                  defaultWsConfig
              let testCaseName = methXmlNode.SelectSingleNode("TestCase/@name").InnerText
              let testCasePath = Path.Combine(nodePath,testCaseName)
              let testCaseNote = methXmlNode.SelectSingleNode("TestCase/Notes").InnerText |> unescXmlSpecialChars
              
              let methodTestInput = 
                match methXmlNode.SelectSingleNodeOpt("TestCase/Input") with
                | None -> ("","")
                | Some(node) -> 
                   let testCaseInputFilename = node.InnerText
                   let testCaseFile = Path.Combine( testCasePath, testCaseInputFilename)
                   let testInputLabel = testCaseInputFilename.Replace("_testinput.xml","")
                   let tcRaw = 
                     let docOpt,err = loadXmlFile testCaseFile
                     if docOpt <> None then
                       xmlPrettyPrint (Option.get docOpt).OuterXml Encoding.UTF8
                     else
                       writeErrLog ("Unable to read test case input :" + testCaseFile)
                       writeErrLog err
                       ""
                     
                   testInputLabel,tcRaw
              
              let methodTestOutput = 
                let tempOpt =  methXmlNode.SelectSingleNodeOpt("TestCase/Output") 
                match tempOpt with
                | None -> ("","")
                | Some(node) -> 
                   let testCaseOutFilename = node.InnerText
                   let testCaseFile = Path.Combine( testCasePath, testCaseOutFilename)
                   let testOutLabel = testCaseOutFilename.Replace("_testoutput.xml","")
                   let tcRaw = 
                     let docOpt,err = loadXmlFile testCaseFile
                     if docOpt <> None then
                       xmlPrettyPrint (Option.get docOpt).OuterXml Encoding.UTF8
                     else
                       writeErrLog ("Unable to read test case output :" + testCaseFile)
                       writeErrLog err
                       ""
                    
                   testOutLabel,tcRaw
                
              let nodeIntvWebService = findWebSvcMethodTreeNode curws.Url name
              let methodNodeOpt = curws.GetWebMethodNode(name)
              if methodNodeOpt <> None then
                 let methodNode = Option.get methodNodeOpt
                 methodNode.WebServiceConfig <- soapReqSettings
                 methodNode.MiscConfig <- nodesettingsMisc
                 
                 let testCaseObj = new MethodTestCase(testCaseName, TestType.Good, methodNode.Name)
                 testCaseObj.Notes <- testCaseNote
                 testCaseObj.AddInput( fst methodTestInput,  snd methodTestInput)
                 testCaseObj.AddExpectedOutput( fst methodTestOutput,  snd methodTestOutput)
                 
                 mergeWithWSMethodNode  nodeIntvWebService methodNode  testCaseObj     
                 if not nodeIntvWebService.IsExpanded then nodeIntvWebService.Toggle() 
              else
                writeWarningLog ("Unable to update method " + name)
                writeWarningLog "Did the wsdl for this web service change?"
            
           )
         
      
            
      let webMgr wsdlEndpoint =
        let mgr = WebSvcManagerOps.createWebSvcManager wsdlEndpoint globalMiscConfig
        mgr
      
      
      let myLock = box 1
      let loadAndCompileWs2 wsdlEndpoint loadTestCases=           
        writeInfoLog ("loading " + wsdlEndpoint)
        let mgr2 = webMgr wsdlEndpoint
        let workerDone (args:RunWorkerCompletedEventArgs) =               
           if args.Error <> null then
               () 
           else
              try
                System.Threading.Monitor.Enter(myLock)
                let (webSvcOpt :IWebSvc option),(elapsed:double) = unbox args.Result
                if webSvcOpt <> None then
                   let webSvc = Option.get webSvcOpt
                   let methodList = webSvc.ReflectedWebMethods |> List.map (fun (name,_) -> name)
                   methodList |> List.iter (fun mName -> 
                        let w = new WebMethNode2(mName)
                        webSvc.AddWebMethodNode(w)
                     )
                   thisForm.AddWebService(webSvc)
                   
                   buildWebServiceTreeNodes webSvc
                   if loadTestCases then
                      loadTestCasesOfWs webSvc       

              finally
                 System.Threading.Monitor.Exit(myLock)  
                 
        let run (mgr:IWebSvcManager) =
           mgr.Download() |> ignore
           if mgr.GenerateProxy() then
              mgr.CompileProxy() |> ignore
           
           let webSvc = mgr.CreateWebServiceModel()
           webSvc
        
           
        
        let invokeWorker = worker run workerDone
        invokeWorker mgr2  //run the function in the background!

                      
      let removeWsClick args =       
         let curWsOpt = curWebServiceOpt()
         if curWsOpt <> None then
           let url = (Option.get curWsOpt).Url
           if DialogResult.Yes = MessageBox.Show(thisForm, "Do you really want to remove\n" + url + "?", "Remove Web Service", MessageBoxButtons.YesNo ) then
             thisForm.RemoveWebService(url)
             let node = thisForm.tvWebServices.Nodes.Find(url, true)
             thisForm.tvWebServices.Nodes.Remove(node.[0])
             ("Removed " + url) |> logSuccess |> logTrigger
      
      //
      //Add a web service
      //
      let addWsClick e =

        for url in uiConfig.MyFavoriteWs do
           if not (addwsForm.cbWsdls.Items.Contains(url)) then
              addwsForm.AddUrl(url)
           
        if addwsForm.ShowDialog(thisForm) = DialogResult.OK then
          loadAndCompileWs2 addwsForm.WsdlEndpoint false
        else 
           ()
        
      thisForm.tsbButtonAdd.Click.Add(addWsClick)
      thisForm.tsBtnRemove.Click.Add(removeWsClick)
         
        
      let loadAppFromUrlList urlist =
        let userMsg = 
          let wsNodes = thisForm.tvWebServices.Nodes.Item(0).Nodes
          if wsNodes.Count > 0 then
             "This will close your current open web service(s). Continue?" 
          else
             ""  
        let promptUser =
          match userMsg with 
          | "" -> DialogResult.OK
          | _ -> MessageBox.Show(thisForm, userMsg, "Close", MessageBoxButtons.OKCancel)
       
        if promptUser = DialogResult.OK then
           clearThisForm()
           //thisForm.pgColorScheme.SelectedObject <- projInstance.Colors            
           for url in urlist do  
             loadAndCompileWs2 url true  
           

        else
          ()
      
      
      thisForm.tsbOpen.Click.Add(fun args ->
         match thisForm.openFileDialog1.ShowDialog() with
         | DialogResult.OK ->
            currentStormProjectFile <- thisForm.openFileDialog1.FileName
          
            //let ext = Path.GetExtension( thisForm.openFileDialog1.FileName)
            // let filename =  (thisForm.openFileDialog1.FileName).Replace(ext ,  ".stormurls")
                      
            let urls = 
              let doc = new XmlDocument()
              doc.Load(currentStormProjectFile)
              let addrs = [for n in doc.SelectNodes("/StormProject/Services/Service/Url") -> n.InnerText]
              addrs |> List.filter (fun add -> add <> "")

            loadAppFromUrlList urls

         | _ -> ()
         
         
      )
      
      
      
      let saveStormProject(fileOpt : string Option) =
         let wsNodes = thisForm.tvWebServices.Nodes.Item(0).Nodes
         
         if wsNodes.Count <= 0 then
           writeErrLog "Please add a web service first." 
         else
         
           
           let proj = new StormProject()          
           //proj.Colors <- unbox<ColorScheme> thisForm.pgColorScheme.SelectedObject  
           
           //Loop through all WebServices
           for n in wsNodes do
             let ws = thisForm.GetWebService(n.Name) |> Option.get            
             proj.AddWSProject(ws)
             
           let origFilter = thisForm.saveFileDialog.Filter
           try
             thisForm.saveFileDialog.Filter <- "storm project(*.stormproj)|*.stormproj"
             let saveShowDialog() = 
               match thisForm.saveFileDialog.ShowDialog() with
               DialogResult.OK ->
                  match thisForm.saveFileDialog.FileName with
                  | "" -> writeErrLog "Filename cannot be empty"
                  | filename -> 
                      proj.Save(filename)
                      writeSuccessLog "Done."
                   
               | _ -> ()
           
             if fileOpt = None then
                saveShowDialog()
             else
                proj.Save(Option.get fileOpt)
            
             
           finally
             thisForm.saveFileDialog.Filter <- origFilter
          
       
       
       
      thisForm.tsbSave.Click.Add(fun args -> saveStormProject(None) )
      
      ///Save the settings before closing 
      (thisForm :> Form).Closing.Add(fun args -> 
        try
          //Save UI Settings
          uiConfig.Save() 
          
          if not (String.IsNullOrEmpty(currentStormProjectFile)) then
            let msg = "Would you like to save the current open project?\nIf you don't save all changes will be lost"
            let res = MessageBox.Show(thisForm, msg, "Save open project", MessageBoxButtons.YesNoCancel) 
            if (DialogResult.Yes = res ) then
               Some(currentStormProjectFile) |> saveStormProject
            elif (DialogResult.No = res ) then
               ()
            else
               args.Cancel <- true
          else
            ()
          

        with
        | _ -> ()
        
        )
      
      
                      
      
      //
      // Tab COntrol Events
      //
      let tabchangeHandler = new TabControlItemChangedHandler(fun e -> 
           match  e.ChangeType with
           | TabControlItemChangeTypes.SelectionChanged ->
              
               //Set the global url, (method, methodInfo) Option variable
               if e.Item <> null && e.Item <> thisForm.tabcQuickTest  && e.Item.ID <> null then
                  if setWsAndMethodInfoFromActiveTab(e.Item.ID) then
                     let ws = curWebServiceOpt() |> Option.get
                     let webmethod = curMethodOpt() |> Option.get
                     setDefaultSoapValues ws webmethod.Name
                     setSoapCallConfig webmethod.MiscConfig webmethod.WebServiceConfig
                                      
               elif e.Item <> null && e.Item = thisForm.tabcQuickTest then
                  let url = thisForm.tvWebServices.SelectedNode.Parent.Name
                  let methodname = thisForm.tvWebServices.SelectedNode.Name
                  buildTreeInput2 url methodname
                   
                          
           | _ -> ()
           
           thisForm.showQuickTestToolStripMenuItem.Enabled <- true;
        )
        
      thisForm.tabcMainTests.add_TabControlItemClosing(
        new TabControlItemClosingHandler( fun e ->
          thisForm.tabcMainTests.DecrementVisibleItemCount()
          if thisForm.tabcMainTests.Items.Count <= 1 then
            e.Cancel <- true;
          
        )
      )
      thisForm.tabcMainTests.add_TabControlItemSelectionChanged(tabchangeHandler)
      //TabControlItemSelectionChanged.
      
      let createNewTestTab() =
         let activeTestReq = thisForm.ActiveTestEditorReq
         ()
      
      
      //
      // Menu Items Events
      //
      thisForm.tsShowConfigSubMenu.Click |> Event.listen( fun _ -> showFormConfig())
      thisForm.tsAddMenuItem1.Click |> Event.listen addWsClick
      thisForm.exitToolStripMenuItem.Click |> Event.listen (fun _ -> thisForm.Close() )
      thisForm.tsRemoveMenuItem.Click |> Event.listen removeWsClick
      thisForm.newWindowToolStripMenuItem.Click |> Event.listen (fun args -> launchNewStormProcess())
      
      
      thisForm.tsAboutFSSubMenu.Click |> Event.listen (fun args ->
         let form = new FSInfo()
         form.ShowDialog() |> ignore
      
       )
      //.Click.Add(fun _ -> showFormConfig())
      //thisForm.tsAddMenuItem1.Click.Add(addWsClick)
      //thisForm.exitToolStripMenuItem.Click.Add(fun _ -> thisForm.Close() )
      //thisForm.tsRemoveMenuItem.Click.Add(removeWsClick)
      //thisForm.newWindowToolStripMenuItem.Click.Add(fun args -> launchNewStormProcess())
//      thisForm.tsMenuItemFSharp.Click.Add(fun args ->
//         let form = new FSInfo()
//         form.ShowDialog() |> ignore
//      
//       )
      
      
      //
      // Web Service ContextMenuStrip Events
      //
      //thisForm.ctxMenuItemRemove.Click.Add(removeWsClick)
      thisForm.ctxMenuItemRemove.Click |> Event.listen removeWsClick
      
      thisForm.ctxMenuItemViewWsdl.Click |> Event.listen (fun args -> 
         thisForm.tabcWsdl.Visible <-true
         thisForm.tabcMainTests.SelectedItem <- thisForm.tabcWsdl
          
      )
      
      let openNewTestTab() =
         let ti = new TabControlItem()
         ti.TabType <- TabControl.TabType.MethodTab
         ti.Height <- thisForm.tabcQuickTest.Height
         ti.Width <- thisForm.tabcQuickTest.Width
         
        
         let name = (curMethodOpt() |> Option.get).Name
         let ws = curWebServiceOpt() |> Option.get
         ti.Title <- name
                          
         let testEditorReq = new TestEditor()
         testEditorReq.Dock <- DockStyle.Fill
         testEditorReq.Name <- "testEditorReq"; 
         testEditorReq.RawText <- "";
         testEditorReq.Size <- new System.Drawing.Size(337, 357);
         
         let tvQuickEdit_AfterSelect_Event = 
            testEditorReq.tvQuickEditInput.AfterSelect 
            |> Event.filter (fun args -> args.Node <> null && args.Node.Tag <> null && args.Node.Level > 1)         
         tvQuickEdit_AfterSelect_Event.Add(quickEditAfterSelect)
         
         testEditorReq.ShowQuickEdit()
         
         testEditorReq.btnGo.Click.Add(btnGoClick)
         testEditorReq.btnSave.Click.Add(fun _ -> btnSaveRawSoap true) 
         testEditorReq.btnXmlView.Click.Add(btnXmlView)
         
         let testEditorResp = new TestEditorResponseControl()
         testEditorResp.Dock <- DockStyle.Fill
         testEditorResp.Name <- "testEditorResp";
         testEditorResp.RawText <- "";
         testEditorResp.Size <- new System.Drawing.Size(302, 357);
         testEditorResp.ShowQuickEdit()
         
         testEditorResp.testEditor1.btnSave.Click.Add(fun _ -> btnSaveRawSoap false) 
         
         
         //testEditorResp.testEditor1.btnXmlView.Click.Add(btnXmlView)
         
         let sc = new SplitContainer()
         sc.Dock <- DockStyle.Fill
         sc.BorderStyle <- BorderStyle.FixedSingle
         sc.SplitterDistance <- sc.Width/2
         sc.Panel1.Controls.Add(testEditorReq)
         sc.Panel2.Controls.Add(testEditorResp)
         
         ti.ID <- ws.GenerateMethodId(name) //ws.Url + "_Storm_" + name
         
         ti.Controls.Add(sc)
         ti
         //thisForm.tabcMainTests.SelectedItem <- ti
      
      let openNewTestRunner (numTests:int) = 
         let ti = new TabControlItem()
         ti.Height <- thisForm.tabcQuickTest.Height
         ti.Width <- thisForm.tabcQuickTest.Width
         
         
         ti.TabType <- TabControl.TabType.TestCaseTab
         
         let curMethod = curMethodOpt() |> Option.get
         let name = curMethod.Name
         let ws = curWebServiceOpt() |> Option.get
         
         //this node is the test case
         let clone = unbox<TreeNode> (thisForm.tvWebServices.SelectedNode.Clone())
         let tcName = MethodTestCase.GetTestCaseName(clone.Text)
         let tc = curMethod.FindTestCase(tcName) |> Option.get
        // tc.RenderView()
         ti.Title <- clone.Text
         ti.ToolTipText <- ti.Title
         
         let runner = TestRunnerFSOps.createRunner [tc] ws thisForm.imgList_All uiConfig
         
         ti.Controls.Add(runner)
         //runner.Dock <- DockStyle.Fill
         ti     
                          
         
      //
      //Test Case ContextMenuStrip Events
      //
      ///thisForm.ctxMenuItemEdit.Click.Add(fun args ->  () )
      
      thisForm.ctxMenuItemRun.Click.Add(fun args ->  
         let tabItem = openNewTestRunner 1
        
         thisForm.tabcMainTests.Items.Add(tabItem) |> ignore  
         thisForm.tabcMainTests.SelectedItem <- tabItem
        
        )
       
      
      
      
      //
      // Web Method ContextMenuStrip Events
      //
      thisForm.ctxMenuItemOpenInTab.Click.Add(fun args -> 
        let tabItem = openNewTestTab()
        thisForm.tabcMainTests.Items.Add(tabItem) |> ignore  
        thisForm.tabcMainTests.SelectedItem <- tabItem
        
        let ws = curWebServiceOpt() |> Option.get
        let c = currentMethodNameAndInfo() |> fst, Option.get (currentMethodNameAndInfo() |> snd)
        let webmethod = ws.GetWebMethodNode(currentMethodNameAndInfo() |> fst) |> Option.get
        let root = buildTreeViewInput c ws //build the treeNode
        setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) (Some(root))
        setTestEditor (thisForm.ActiveTestEditorReq) (Some("")) None 
        
        setDefaultSoapValues ws webmethod.Name  
        setSoapCallConfig webmethod.MiscConfig webmethod.WebServiceConfig
                
      )
      
          
      let globalHandler = new ThreadExceptionEventHandler(fun sender args ->
        writeExceptionLog args.Exception
        progressBarStart false
         )
      Application.add_ThreadException(globalHandler)
      
              
         
      
      
      
        