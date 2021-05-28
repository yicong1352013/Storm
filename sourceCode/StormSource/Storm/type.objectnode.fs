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
   open System.Reflection
   open System.Web.Services.Protocols
   open Storm.Types
   open Storm.Types.ReflectionOps
   open Storm.Utilities
   open System.Windows.Forms
   
   
    
   type ObjectNode(underlyingType : Type, obValInstType : InstanceTypes, name :string ) = 
      let innerTree = new TreeNode(name)
      let mutable outputTree = new TreeNode()
      let objVal = getInnerValueOf obValInstType
          
      member x.ReadType(readFunc : (obj -> TreeNode -> string -> TreeNode)) =
         let outTree=  readFunc objVal innerTree name
         outputTree <- outTree    
      
      member x.OutputTree 
         with get() = outputTree
      
      member x.Name
         with get() = name
   
   module WebMethodNodeOps = 
     
      let createObjectNode (underlyingType : Type) (obValInstType : InstanceTypes) (name :string) =
        new ObjectNode(underlyingType, obValInstType, name)
        
      let createSoapHeader (m:MethodInfo ) direction=
         let soapHead = 
            let attr = List.of_array ( m.GetCustomAttributes(typeof<SoapHeaderAttribute>, true) )
         
            let y = 
              attr |> List.choose (fun x ->
      
                        let soapAttr = x :?> SoapHeaderAttribute
                        let ok =
                          match soapAttr.Direction with
                          | SoapHeaderDirection.InOut when (direction = SoapHeaderDirection.In || direction = SoapHeaderDirection.Out) -> true
                          | SoapHeaderDirection.In when direction = SoapHeaderDirection.In -> true
                          | SoapHeaderDirection.Out when direction = SoapHeaderDirection.Out -> true
                          | _ -> false        
                        if ok then
                          let temp = m.DeclaringType.GetProperty(soapAttr.MemberName) 
                          if temp <> null then
                             Some(temp)
                          else
                             None
                        else
                           None
                        
                     )
            y
         soapHead
      
      let setSoapHead (proxy : HttpWebClientProtocol) (met:MethodInfo) headName headVal=
        let declaringType = met.DeclaringType
        let propInfo = declaringType.GetProperty(headName)
        propInfo.SetValue(proxy, headVal,null)
        ()
       // declaringType.GetProperty(property.Name).SetValue (proxy, property.ReadChildren());
        
        //()
      
      let populateSoapHeader (proxy : HttpWebClientProtocol) direction =
         fun x -> createSoapHeader x direction |> List.choose (fun propInfo ->
            if propInfo <> null then
               let propVal= 
                 match propInfo.GetValue(proxy, null) with
                 | temp when temp <> null -> MyObjectHandle temp
                 | _ -> createInstanceFromType(propInfo.PropertyType)
               Some(propInfo.Name , propInfo.PropertyType, propVal)   
            else
               None
               )
      
      let populateSoapBody (m: MethodInfo) =
         let mParams = List.of_array (m.GetParameters() ) 
         mParams 
            |> List.filter (fun y -> not y.IsOut) 
            |> List.choose (fun x -> 
                 let pType = x.ParameterType
                 let getparamType pType2 =
                    match pType with
                    |  _ when pType.IsByRef -> pType.GetElementType()
                    | _ -> pType
                 
                 let paramType = getparamType pType
                 let instance = ReflectionOps.createInstanceFromType paramType
                 Some (x.Name , paramType , instance)
                 
            )
            
      // TODO : needs to figure out the serialization binder
      let soapStringToObject str = 
        let formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
        let strm = GenHelper.utf8stringToStream str
        let o = formatter.Deserialize(strm)
        o