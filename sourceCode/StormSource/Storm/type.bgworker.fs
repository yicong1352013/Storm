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
   open System.ComponentModel
   open System.Threading
   open Storm.Types
   open Microsoft.FSharp.Control.CommonExtensions
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   
   type DelegatingBgWorker<'a,'b> (f: 'a -> 'b ) = 
      let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
      let logDone, (logEventDone : IEvent<RunWorkerCompletedEventArgs>) = Event.create()
      
      let mutable dtStart = DateTime.MinValue
      let mutable dtEnd = DateTime.MinValue
      let worker = new BackgroundWorker()
      let syncContext = SynchronizationContext.Current
      let mutable fArgsOpt :Option<'a> = None
     
      let triggerParent msg= 
        syncContext.Post(SendOrPostCallback (fun _ -> 
            logTrigger msg 
            ), null)
      
      let mutable fRes : 'b option = None
      
      let run funcArgs =
        match funcArgs with
        | Some(arg) ->
        
            worker.RunWorkerCompleted.Add(fun complArgs -> logDone complArgs )
            
            worker.DoWork.Add(fun dArgs -> 
               if ( box arg :? ILog) then
                  let _, evt = ( (box arg) :?> ILog).LogEvt
                  evt.Add(fun msg -> triggerParent msg)
               
               fRes <- Some (f arg )
               dtEnd <- DateTime.Now               
               dArgs.Result <- box (fRes,  (dtEnd-dtStart).TotalSeconds)   
                     
             )            
            worker.RunWorkerAsync()
        | _ -> () 
      
      member x.FunctionResult 
         with get() = fRes
      member x.FunctionArguments       
        with get() = fArgsOpt
        and set (args :'a) = fArgsOpt <- Some(args)
       member x.ElapsedTime 
         with get() = (dtEnd-dtStart).TotalSeconds
          
      member x.RunWorkerAsync() =
         dtStart <- DateTime.Now
         run x.FunctionArguments
     
      member x.CancelAsync() =
         worker.CancelAsync()
      member x.LogEvent = logEvent
      member x.DoneEvent = logEventDone
   
   module DBackgroundWorkerImpl =
      let createDWorker (funcToRun: 'a -> 'b) funcWhenDone funcLogEvent=
         let bg = new DelegatingBgWorker<_,_>(funcToRun)
         //bg.FunctionArguments <- methodParams
         bg.DoneEvent.Add(funcWhenDone)
         bg.LogEvent.Add(funcLogEvent)                    
         
         fun (funcToRunParam : 'a ) ->
           bg.FunctionArguments <- funcToRunParam
           bg.RunWorkerAsync()
           bg.FunctionResult
      
      
         
     