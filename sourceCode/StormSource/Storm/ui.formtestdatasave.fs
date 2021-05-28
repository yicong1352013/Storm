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
   open Storm.UI
   open Storm.Types
   open Storm.Utilities.GenHelper
   
   type SaveMode =
   | File = 0
   | TestCaseInput =1
   | TestCaseExpectedOutput = 2
   | Both = 3
   
   [<AbstractClass>]
   type FFormTestDataSave() = 
     inherit FormTestDataSave()
     
     //Local let bindings
     let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
     let mutable mode = SaveMode.TestCaseInput
     
     abstract TestInputLabel : string with get,set
     abstract TestCaseLabel : string with get,set
     abstract TestCaseList : string list with get, set
     //abstract TreeContentToSave : TreeNode with get, set
     abstract RawContentToSave : string with  get, set
     
     abstract TestCaseNote : string
     //abstract TDTreeNode : TreeNode
     member x.Mode 
       with get() = mode
       and set s = mode <- s
       
     interface ILog with       
        member x.LogEvt = (logTrigger, logEvent)
    
   module FFormTestDataSaveOps =
      
      let private createFormTDSave =    
        let strContentToSave = ref ""
        let tnContentToSave:TreeNode ref= ref null
        
        let tcList = new ResizeArray<string>()
        {
         new FFormTestDataSave() with
           member x.TestCaseList 
             with get()  = ResizeArray.to_list tcList
             and set mlist =
               tcList.Clear()
               for n in mlist do tcList.Add(n)
           member x.RawContentToSave 
             with get() = !strContentToSave  
             and set s = strContentToSave := s
         
           member x.TestCaseNote
             with get ()  = x.rtbNote.Text
           member x.TestCaseLabel 
             with get() = x.cbTestCaseList.Text
             and set s = x.cbTestCaseList.Text <- s
           member x.TestInputLabel 
             with get() = x.textBox1.Text //name of the request Input
             and set s = x.textBox1.Text <- s
        }  
      
      let thisForm = createFormTDSave
      thisForm.Load.Add(fun _ ->
          if thisForm.Mode = SaveMode.File then
            thisForm.rbFile.Checked <- true
          else
            if thisForm.Mode = SaveMode.TestCaseInput then
               thisForm.groupBox1.Text <- "Save soap request as..."
               thisForm.lblSoapName.Text <- "Label request as"
            if  thisForm.Mode = SaveMode.TestCaseExpectedOutput then
               thisForm.groupBox1.Text <- "Save soap response as..."
               thisForm.lblSoapName.Text <- "Label response as"
            
            thisForm.ActiveControl <- thisForm.rtbNote 
            thisForm.cbTestCaseList.Items.Clear()
            thisForm.TestCaseList 
            |> List.iter (fun x -> 
               thisForm.cbTestCaseList.Items.Add(x ) |> ignore ))
         
                
      let rbTestDataChecked = 
        thisForm.rbProjTestData.CheckedChanged
        |> Event.filter (fun args -> thisForm.rbProjTestData.Checked =  true) 
     
      thisForm.rtbNote.Click.Add(fun _ -> thisForm.rtbNote.Focus() |> ignore )
      thisForm.btnOk.Click.Add(fun args ->
          if (thisForm.rbProjTestData.Checked) then
            () //save into the project
          else
            thisForm.TestCaseLabel <- ""
            thisForm.TestInputLabel <- ""
            if DialogResult.OK = thisForm.saveFileDialog1.ShowDialog(thisForm) then
              let filename = thisForm.saveFileDialog1.FileName
              createOrOverwriteFile thisForm.RawContentToSave filename
            else
              ()
        )
        
   