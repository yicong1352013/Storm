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
   open System.Web.Services
   open System.Web.Services.Protocols
   open System.Web.Services.Description
   open System.Web.Services.Discovery
   open System.Windows.Forms
   open System.Xml
   open System.Net
   open System.Reflection
   open System.Xml.Schema
   open System.Xml.Serialization
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigurationImpl
   open Storm.CS.Lib.Soap
   
   type LastOutput = 
     {
      mutable TvInputNode : TreeNode
      mutable TvOutputNode : TreeNode 
      mutable SoapResp : string
      mutable SoapReq : string
      mutable RequestHead :string
     }
   
   type WebMethNode2(name:string) =
     let mutable miscCfg =  defaultConfig
     let mutable wsCfg = defaultWsConfig
     let testCases = new ResizeArray<MethodTestCase>()
      
     let lastOut = 
      {
         TvInputNode = new TreeNode()
         TvOutputNode = new TreeNode()
         SoapResp = ""
         SoapReq = ""
         RequestHead = ""
      }
     
     member x.Name = name
     member x.MiscConfig   //user editable
       with get() = miscCfg
       and set s = miscCfg <- s
     member x.WebServiceConfig   //user editable
       with get() = wsCfg
       and set s = wsCfg <- s 
     member x.LastRunsParameters 
       with get () = lastOut
     member x.AllTestCases
        with get() =  testCases |> ResizeArray.to_list |> List.map (fun y -> y.Name)
     member x.AddOrUpdateTestCase(tc:MethodTestCase) =
        let tempOpt = x.FindTestCase(tc.Name)
        if tempOpt <>  None then
           //update the input list and Notes
           let case:MethodTestCase = Option.get tempOpt
           case.Notes <- String.Format ("{0}\n---------------\n{1}",case.Notes, tc.Notes )
           case.AddInput(fst tc.Input, snd tc.Input)
           
           //tc.InputList |> List.iter (fun (label,tree) -> case.AddOrUpdateInput(label,tree) )
           
        else   
           testCases.Add(tc)
     member x.FindTestCase (name) : MethodTestCase option=
      testCases |> ResizeArray.tryfind (fun y -> y.Name = name)
     member x.RemoveTestCase(name) =
       let tcOpt = x.FindTestCase(name)
       if tcOpt <> None then
         testCases.Remove(Option.get tcOpt) |> ignore
     member x.RenderTestCaseView(name:string) =
       let tempOpt = x.FindTestCase (name)
       if tempOpt <> None then
         let case = Option.get tempOpt
         Some(case.RenderView())
       else
         None