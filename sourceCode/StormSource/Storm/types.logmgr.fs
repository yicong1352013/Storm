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
   
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Utilities
   open System
   open System.Windows.Forms
   open System.Drawing
   
   [<AbstractClass>]
   type LogManager( ctr : System.Windows.Forms.RichTextBox )  =
   
      let mutable startTime = DateTime.MinValue
      
      abstract Header : string
      abstract Footer : string
      abstract Log :  LogMessage -> unit
      member x.StartTime 
        with get () = startTime
        and set dt =  startTime <- dt
      
   
   module LogManagerImpl =
     
     //TODO : check if this usingLog will work!
     let usingLog (lg:LogManager) =
        fun f lMsg -> 
           let start = DateTime.Now
           lg.StartTime <- start
           try 
              f 
           finally
              lg.Log(lMsg)
              
     
     let createLogManager ctr =
       {
          new LogManager(ctr) with 
            member x.Header = "Started on : "
            member x.Footer = "Elapsed (sec) : "
            
            member x.Log (lMsg) = 
              let getColor logMsg =
                 match logMsg.LogType with
                 | LogTypeEnum.Success -> Color.Blue,logMsg                    
                 | LogTypeEnum.Error -> Color.Red, logMsg
                 | LogTypeEnum.Warning -> Color.DarkGoldenrod, logMsg
                 | LogTypeEnum.Info -> Color.Black,logMsg
                 | LogTypeEnum.Debug -> 
                     ctr.Font <- new Font(ctr.Font, FontStyle.Italic)
                     Color.DarkGray, logMsg
                 | _ -> Color.Red, (LogHelper.logErr "Invalid Log Type")
                    
              let textColor, temp = getColor lMsg   
              
              ctr.Focus() |> ignore
              ctr.SelectionColor <- textColor
              
              let getMessage =
                 match x.StartTime with
                 | _ when x.StartTime = DateTime.MinValue -> temp.Message
                 | _ -> 
                    let s = String.Format("{0} {1}", x.Header, x.StartTime.ToString())
                    let ft = String.Format("{0} {1}", x.Footer, (DateTime.Now - x.StartTime).TotalSeconds)
                    String.Format("{0}\n   {1}\n{2}", s  , temp.Message, ft) 
              
              if (lMsg.LogType <> LogTypeEnum.Debug) then
                  ctr.AppendText(getMessage + "\n")
                  ctr.Font <- new Font(ctr.Font, FontStyle.Regular)
              else
                  let reader = ConfigReaderImpl.createAppSettingsReader
                  let printIfDebugging = 
                     match reader.Read ConfigReaderImpl.readAppSettings "Debugging" with
                     | Some(s) when s.ToLower() = "true" ->
                        ctr.AppendText(getMessage + "\n")
                     | _ -> ()
                  printIfDebugging

              ctr.Update()     
              ctr.ScrollToCaret()              
             
       }