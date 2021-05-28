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
   open Storm.Utilities
   
   [<AbstractClass>]
   type FFormAddWS() = 
     inherit FormAddWS()
     
     //Local let bindings
     let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
     
     abstract WsdlEndpoint : string with get, set
     abstract AddUrl : string -> unit
     //Implementation of ILog interface
     interface ILog with       
        member x.LogEvt = (logTrigger, logEvent)
      
     
   
   module FormAddWsOps =
      
      let private createFormAddWs() =
        {
          new FFormAddWS() with
            member x.WsdlEndpoint
              with get () = x.cbWsdls.Text
              and set(s) = x.cbWsdls.Text <- s   
            member x.AddUrl(s) =
              x.cbWsdls.Items.Add(s) |> ignore         
        }
      
      let thisForm = 
        let temp = createFormAddWs()
        temp.ActiveControl <- temp.cbWsdls
        temp
      
      
      let logTrigger, logEvent = (thisForm :> ILog).LogEvt
      
      thisForm.Load.Add(fun _ ->
           if thisForm.cbWsdls.Items.Count > 0 then
              thisForm.cbWsdls.SelectedIndex <- 0
       )
           
      let private btnGo_Click =
        thisForm.btnGo.Click.Add(fun _ -> 
           logTrigger( LogHelper.logSuccess thisForm.cbWsdls.Text ) 
           
           if not (thisForm.cbWsdls.Items.Contains(thisForm.cbWsdls.Text)) then
             thisForm.cbWsdls.Items.Add(thisForm.cbWsdls.Text) |> ignore
           thisForm.ActiveControl <- thisForm.cbWsdls         
           thisForm.DialogResult <- DialogResult.OK
           
           )
      
         
      //Events are first class values! Isn't that awesome!!! :)     
      let rtbWsdlEndpoint_escPressedEvent = thisForm.cbWsdls.KeyUp |> Event.filter (fun args -> args.KeyCode = Keys.Escape)
      rtbWsdlEndpoint_escPressedEvent.Add(fun args ->
              //logTrigger( LogHelper.logInfo  )          
              thisForm.DialogResult <- DialogResult.Cancel
              thisForm.cbWsdls.Text <- ""
              args.Handled <- true
         )