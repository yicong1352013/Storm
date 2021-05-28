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
   open System.ComponentModel
   open System.Text
   open Storm.UI
   open Storm.Types
   open Storm.Utilities
   
   
   
   type FFormQuickEdit() = 
     inherit FormQuickEdit()
     
     //Local let bindings
     let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
     
     //Implementation of ILog interface
     interface ILog with       
        member x.LogEvt = (logTrigger, logEvent)
      
     
   
   module FFormQuickEditOps =
      
      let private createFormQuickEdit() =  new FFormQuickEdit() 
      
      let thisForm = 
        let temp = createFormQuickEdit()
        temp.ActiveControl <- temp.pgInput
        temp
      
      thisForm.btnLoadFile.Click 
        |> Event.listen (fun _ -> 
           thisForm.lblLoadFile.Text <- ""
           match thisForm.openFileDialog1.ShowDialog() with
           | DialogResult.OK ->
                let file = thisForm.openFileDialog1.FileName
                let bytes = GenHelper.fileToByteArray file
                if bytes.Length > 0 then
                   let wrapper = new QuickEditWrapper<_>(box bytes)
                   thisForm.pgInput.SelectedObject <- wrapper 
                   thisForm.lblLoadFile.Text <- System.IO.Path.GetFileName(file) + " loaded"
                ()
           | _ -> ()
           //GenHelper.fileToByteArray(
         )
        
      thisForm.btnOk.Click.Add(fun args -> thisForm.DialogResult <- DialogResult.OK)
      thisForm.btnCancel.Click.Add(fun args -> thisForm.DialogResult <- DialogResult.Cancel)
      thisForm.cbPolymorphTypes.SelectedIndexChanged.Add(fun _ -> 
           let selectedType : Type = GenHelper.cast thisForm.cbPolymorphTypes.SelectedItem
           let instance = (ReflectionOps.createInstanceFromType selectedType) |> ReflectionOps.getInnerValueOf
           
           let wrapper = new QuickEditWrapper<_>(instance)
           if thisForm.pgInput.SelectedObject <> null then
              let wrapperPrev : QuickEditWrapper<_> = GenHelper.cast thisForm.pgInput.SelectedObject
              wrapper.IsNull <- wrapperPrev.IsNull
              
           thisForm.pgInput.SelectedObject <- wrapper 
        )
      
      let enterKyUp = thisForm.KeyUp |> Event.filter (fun args -> args.KeyCode = Keys.Enter)
      let escKyUp = thisForm.KeyUp |> Event.filter (fun args -> args.KeyCode = Keys.Escape)
      enterKyUp.Add(fun args -> thisForm.DialogResult <- DialogResult.OK)
      escKyUp.Add(fun args -> thisForm.DialogResult <- DialogResult.Cancel)
      
      let logTrigger, logEvent = (thisForm :> ILog).LogEvt
           
     