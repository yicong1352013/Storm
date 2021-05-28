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
   open System.Drawing
   open System.Drawing.Design
   open System.Windows.Forms
   open System.Text
   open System.IO
   open Microsoft.XmlDiffPatch
   //open System.Windows.Forms.Html
   open Storm.UI
   open Storm.Types
   open Storm.Utilities.GenHelper
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigReaderImpl
   open System.ComponentModel
   
   
   type TestRunnerFS( tests : MethodTestCase list, webSvc:IWebSvc, imgList: ImageList) = 
     inherit TestRunner()
     
     let rootNode =
       let temp = new TreeNode("Tests for " + (List.hd tests).MethodName)
       temp
     
     let cfg = 
        let anymethod = webSvc.GetAllWebserviceMethods() |> List.hd
        let mth = webSvc.GetWebMethodNode(anymethod) |> Option.get
        mth.MiscConfig, mth.WebServiceConfig
        
     let mutable currenTestInput = ""
     let tcOpt() = 
       tests |> List.tryfind (fun tc ->  (fst tc.Input) = currenTestInput )
     
     let worker (func: 'a -> 'b ) funcDone writeExceptionLog writeInfoLog  =
        let bg = new DelegatingBgWorker<_,_>(func)  
        //bg.LogEvent.Add(fun m -> logTrigger (m)  )      
        bg.DoneEvent.Add(funcDone)
        bg.DoneEvent.Add(fun (args:RunWorkerCompletedEventArgs) -> 
           if args.Error <> null then
               writeExceptionLog args.Error 
           else
              let (_ :'b option),(elapsed:double) = unbox args.Result
              (formatElapsed elapsed) |> writeInfoLog
          )
        
        fun funcArgs -> 
           bg.FunctionArguments <- funcArgs
           bg.RunWorkerAsync() 
     
     member x.SaveExpectedResponse() =
        match tcOpt() with
        | None -> ()
        | Some(tc) ->
            if (stringToXmlDoc (x.testCaseEditorExpectedResp.RawText) ) <> None then 
              tc.AddExpectedOutput( currenTestInput, x.testCaseEditorExpectedResp.RawText)  
              let respBrowser = xmlForBrowser (snd tc.ExpectedOutput )
              x.testCaseEditorExpectedResp.RawText <- snd tc.ExpectedOutput
              x.testCaseEditorExpectedResp.BrowserText <- respBrowser
              x.testCaseEditorExpectedResp.RefreshView()
              UICommon.showNotifier x "Saved"
            else
               UICommon.showNotifier x "Invalid expected xml response"
            
     member x.SaveRequest() =
        match tcOpt() with
        | None -> ()
        | Some(tc) ->
           if (stringToXmlDoc (x.testCaseEditorRequest.RawText) ) <> None then 
              tc.AddInput(currenTestInput, x.testCaseEditorRequest.RawText)
              UICommon.showNotifier x "Saved"
           else
             UICommon.showNotifier x "Invalid xml request"
     member x.SaveNotes() =
        match tcOpt() with
        | None -> ()
        | Some(tc) ->
            tc.Notes <- x.testCaseEditorNotes.RawText
            UICommon.showNotifier x "Saved"

     member x.PromoteActualResponse() =
        match tcOpt() with
        | None -> ()
        | Some(tc) ->
            if (stringToXmlDoc (x.testCaseEditorActualResp.RawText) ) <> None then 
              tc.AddExpectedOutput( currenTestInput, x.testCaseEditorActualResp.RawText)    
              let respBrowser = xmlForBrowser (snd tc.ExpectedOutput )
              x.testCaseEditorExpectedResp.RawText <- snd tc.ExpectedOutput
              x.testCaseEditorExpectedResp.BrowserText <- respBrowser
              x.testCaseEditorExpectedResp.RefreshView()
              UICommon.showNotifier x "Done"
            else
              UICommon.showNotifier x "Invalid actual xml response"
              
     member x.WriteErrorLog (err) =
        x.rtbErrorsTestRunner.Text <-  x.rtbErrorsTestRunner.Text + err + "\n"      
     member x.WriteExceptionLog (exc:Exception) =
        x.WriteErrorLog(exc.ToString())        
     member x.WriteInfoLog info = 
       x.rtbLogTestRunner.Text <- x.rtbLogTestRunner.Text + info + "\n"
       
     member private x.CreateNodes() =
       let render (tr2:TreeNode) (tc:MethodTestCase) =
           let node = tc.RenderView()
           tr2.Nodes.Add(node) |> ignore
           tr2
       List.fold_left render rootNode tests
       
     member x.ExecuteTests() =
       x.lnkLabelViewErr.Visible <-false
       tests |> List.iter(fun tc -> 
       
          let mt = webSvc.GetWebMethodNode(tc.MethodName) |> Option.get
          x.ExecuteSingleTest(tc, mt)
        )
       
     member x.WebService = webSvc
     
     member x.FindTestCase(name:string) =
       tests |> List.tryfind(fun t -> t.Name = name  )
     member x.RenderView() =
        x.tvTests.ImageList <- imgList
        x.tvTests.Nodes.Add(x.CreateNodes()) |> ignore
        x.tvTests.ExpandAll()
    
     member x.RenderTestCase(tc : MethodTestCase) =
        let inputName, input = tc.Input
        currenTestInput <- inputName 
        x.testCaseEditorNotes.RawText <- tc.Notes     
        if File.Exists(defaultsXslFile) then
           let xmlForBrowser = webBrowserXmlText defaultsXslFile
           let reqBrowser = xmlForBrowser input 
           x.testCaseEditorRequest.RawText <- input
           x.testCaseEditorRequest.BrowserText <- reqBrowser
           x.testCaseEditorRequest.ShowBrowser()
                               
           if (stringToXmlDoc (snd tc.ExpectedOutput) ) <> None then 
              let respBrowser = xmlForBrowser (snd tc.ExpectedOutput )
              x.testCaseEditorExpectedResp.RawText <- snd tc.ExpectedOutput
              x.testCaseEditorExpectedResp.BrowserText <- respBrowser
              x.testCaseEditorExpectedResp.ShowBrowser()
                   
     member x.ShowDiff() =
        if (tcOpt() <> None) then
           let tc = Option.get (tcOpt())
           let sb = new SimpleBrowser(tc.DiffInHtml)
           sb.Show()
           
     member x.SaveResponse(resp:string) =
        if (tcOpt() <> None) then
           let tc = Option.get (tcOpt())
           if (resp.Contains("soap:Fault")) then
               //x.progBar.Increment(1, false)
               x.WriteErrorLog(resp)
//           else
//               x.progBar.Increment(1, true)
           tc.AddActualOutput( fst tc.ExpectedOutput, resp )
        else
           ()
     
     member x.RenderResults(tc:MethodTestCase) =      
       let actualresp = snd tc.ActualOutput
       let req = snd tc.Input 
       let expectedresp = snd tc.ExpectedOutput
       
       if File.Exists(defaultsXslFile) then
                
            if (stringToXmlDoc actualresp) <> None then 
                let respBrowser = xmlForBrowser actualresp                
                x.testCaseEditorActualResp.RawText <- xmlPrettyPrint actualresp Encoding.UTF8
                x.testCaseEditorActualResp.BrowserText <- respBrowser
                x.testCaseEditorActualResp.ShowBrowser()
            
            //Compare the results if we have an expected respose. Otherwise, check the actual response
            // for a soap fault
            if not (String.IsNullOrEmpty expectedresp) then
              let errOpt = getDiffIfError expectedresp actualresp
              if errOpt <> None then
                let htmlErr = Option.get errOpt
                tc.DiffInHtml <- htmlErr
                x.lnkLabelViewErr.Visible <-true
                x.progBar.Increment(1, false)
                //UICommon.showNotifier x "Test successful"
              else
                x.lnkLabelViewErr.Visible <-false
                x.progBar.Increment(1, true)
             
            elif (actualresp.Contains("soap:Fault")) then
               x.progBar.Increment(1, false)
            else
               x.progBar.Increment(1, true)
               
              
       ()
       
     
     member x.ExecuteSingleTest(tc: MethodTestCase, methodNode: WebMethNode2) =                
         System.Threading.Monitor.Enter(box x)   
         let inputName, inputXml = (fst tc.Input), x.testCaseEditorRequest.RawText //must be the text in the 
         //currenTestInput <- inputName
         if (String.IsNullOrEmpty(inputXml)) then
           x.WriteErrorLog(String.Format("Input file \"{0}\" is empty", inputName))
         else
           let proxy = webSvc.ConfiguredClientProxy (Some(methodNode.MiscConfig))
           let invokeWs = MethodInvokerOps.invokeViaHttp inputXml methodNode.MiscConfig methodNode.WebServiceConfig
           let workerDone (args:RunWorkerCompletedEventArgs) =
              System.Threading.Monitor.Exit(box x)
              
              if (args.Error <> null) then
                 x.WriteExceptionLog args.Error
                 x.progBar.Increment(1, false)
              else
                  //  x.WriteInfoLog("Success")
                 //   x.progBar.Increment(1, true)
                    if (List.length tests) = 1 then
                       x.RenderResults(List.hd tests)
              
                 
                    
           let invokeWorker = worker invokeWs workerDone x.WriteExceptionLog x.WriteInfoLog
         
           x.lblCurenttest.Text <- String.Format( "Executing Test : {0}, Input : {1} ", tc.Name, inputName)
           x.WriteInfoLog "Executing test..."
           
           invokeWorker proxy
         
           
         ()
     
   module TestRunnerFSOps =
        
        let runnerLabel = "TestRunner"
        let private initRunner (runner : TestRunnerFS) numTests (uiConfig:UIConfig)=
           runner.testCaseEditorActualResp.Label <- "Actual Response" 
           runner.testCaseEditorActualResp.SaveToolTip <- "Promote to expected response" 
           runner.testCaseEditorActualResp.btnEditRaw.Enabled <- false;
           runner.testCaseEditorActualResp.ShowEdit()
           
           runner.testCaseEditorActualResp.ColorSoap <- uiConfig.ColorRawSoap
           runner.testCaseEditorExpectedResp.Label <- "Expected Response" 
           runner.testCaseEditorExpectedResp.ShowEdit()
           runner.testCaseEditorExpectedResp.ColorSoap <- uiConfig.ColorRawSoap
           
           runner.testCaseEditorRequest.Label <- "Test Input" 
           runner.testCaseEditorRequest.ShowEdit()
           runner.testCaseEditorRequest.ColorSoap <- uiConfig.ColorRawSoap
           
           runner.Name <-  runnerLabel
           runner.Dock <- DockStyle.Fill
           runner.progBar.Maximum <- numTests 
           runner.btnRunOrStop.Click.Add(fun args -> 
             runner.progBar.Restart()
             runner.ExecuteTests() )
           
           //REQUEST - INPUT
           runner.testCaseEditorRequest.btnSave.Click.Add(fun args -> runner.SaveRequest())
           runner.testCaseEditorRequest.btnXmlView.Click.Add(fun args -> 
                let browser = xmlForBrowser runner.testCaseEditorRequest.RawText
                runner.testCaseEditorRequest.ShowBrowser(browser)
              )
           
            //EXPTECTED RESPONSE
           runner.testCaseEditorExpectedResp.btnSave.Click.Add(fun args -> runner.SaveExpectedResponse())
           runner.testCaseEditorExpectedResp.btnXmlView.Click.Add(fun args -> 
                let browser = xmlForBrowser runner.testCaseEditorExpectedResp.RawText
                runner.testCaseEditorExpectedResp.ShowBrowser(browser)
              )
           
           
           //ACTUAL RESPONSE
           runner.testCaseEditorActualResp.btnSave.Click.Add(fun args -> runner.PromoteActualResponse())
           
           //Notes
           runner.testCaseEditorNotes.btnSave.Click.Add(fun args -> runner.SaveNotes())
           
           
           runner.lnkLabelViewErr.Click.Add(fun args -> 
               runner.ShowDiff()
             )
             
           
           
           
           let tvTests_TestCaseInput_AfterSelect_Event = 
                runner.tvTests.AfterSelect 
               |> Event.filter(fun args -> args.Node <> null && args.Node.Tag <> null)
               |> Event.filter(fun args -> 
                      let nodeType = unbox<TreeNodeTag> args.Node.Tag   
                      nodeType.Type = TreeNodeType.TestCase )
           
           tvTests_TestCaseInput_AfterSelect_Event.Add(fun args ->
              let tc = MethodTestCase.GetTestCaseName(args.Node.Name) |> runner.FindTestCase |> Option.get
              runner.RenderTestCase(tc)
              

            )
            
         
           
        let createRunner tests webSvc imgList uiConfig =
          let temp = new TestRunnerFS(tests, webSvc, imgList)
          temp.RenderView()
          initRunner temp (List.length tests) uiConfig
          
          if (List.length tests > 0) then
             temp.RenderTestCase(tests |> List.hd)
          temp
          
        
       
                
       //wbSoapInput
       //wbActualersp
       //wbexpectedResp
       
       
     
     

