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
   
   
   type TestType =
   | Good = 1
   | Failing = 2
   

   type MethodTestCase(caseName:string, scenType:TestType, methodName:string) =
     let mutable testNotes = ""
     let mutable expectedOutput = ("","")
     let mutable actualOutput = ("","")
     
     let mutable testinput = ("","")
     let inputList = new ResizeArray<_>() //contains (name, soapinput) Tuple items
     let mutable diff = "";
     
     member x.DiffInHtml
        with get() = diff
        and set s = diff <- s
        
     member x.MethodName = methodName  
     member x.Name = caseName
     member x.Notes 
       with get() = testNotes
       and set s = testNotes <- s
     member x.ScenarioType 
       with get() = scenType
     member x.Input 
        with get() = testinput 
        
        
     member x.ExpectedOutput 
       with get() = expectedOutput
     member x.ActualOutput 
       with get() = actualOutput
       
     member x.AddInput(name:string, soapInput:string) =
       testinput <- name,soapInput
     member x.AddExpectedOutput(name:string, soapInput:string) =
       expectedOutput <- name,soapInput
     member x.AddActualOutput (name:string, soapOutput:string) =
       actualOutput <- name,soapOutput
       
     static member GetTestCaseName(s:string) =
       let name = s.Replace("Test Case:","").Trim()
       name
     static member GetTestCaseInputName(s:string) =
       let name = s.Replace("Input:","").Trim()
       name
     static member GetTestCaseOutputName(s:string) =
       let name = s.Replace("Output:","").Trim()
       name
       
     member x.RenderView() =
       let temp = new TreeNode(x.Name , 17,17) //see imageList_all in FormMain
       temp.Name <- x.Name 
       temp.Text <- "Test Case: " + x.Name 
       temp.Tag <-  { Type = TreeNodeType.TestCase; ObjectType = None; ObjectParentType = None; OriginalValue =None; ModifiedValue = None }
 
       let addInputNode() =
          match testinput with
          | ("","") -> ()
          | (m,n) -> 
              let inputNode = new TreeNode("Input: " + m ,10, 20)
              inputNode.Tag <- { Type = TreeNodeType.TestInput; ObjectType = None; ObjectParentType = None; OriginalValue = Some(box (n)); ModifiedValue = None }
              temp.Nodes.Add(inputNode) |> ignore
       
       let addExpectedNode() =
          match expectedOutput with
          | ("","") -> ()
          | (m,n) -> 
                let expectedOutputNode = new TreeNode("Output: " + m ,11, 11)
                expectedOutputNode.Tag <- { Type = TreeNodeType.TestExpectedResult; ObjectType = None; ObjectParentType = None; OriginalValue = Some(box (n)); ModifiedValue = None }
                temp.Nodes.Add(expectedOutputNode) |> ignore
          
       (*let addExpectedNode() =
          match expectedOutput with
          | ("","") -> ()
          | (m,n) ->           
		     let expectedOutputNode = new TreeNode(m ,11, 11)
             expectedOutputNode.Tag <- { Type = TreeNodeType.TestExpectedResult; OriginalValue = Some(box (n)); ModifiedValue = None }
             temp.Nodes.Add(expectedOutputNode) |> ignore*)
       
       addInputNode()
       addExpectedNode()
       
       if x.Notes.Trim() <> "" then
          let noteNode = new TreeNode("Note(s)",18,18)
          noteNode.Tag <- { Type = TreeNodeType.TestNote;  ObjectType = None; ObjectParentType = None; OriginalValue =None; ModifiedValue = None }
                
          noteNode.ToolTipText <- x.Notes                   
          temp.Nodes.Add(noteNode) |> ignore 
       temp
   
       
    