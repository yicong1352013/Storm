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
   open System.Data
   open System.Reflection
   open System.Web.Services.Protocols
   open System.Windows.Forms
   open System.Runtime.Remoting
   open System.Xml
   open Storm.Utilities.GenHelper
   open Storm.Utilities.LogHelper
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigurationImpl
   
   //ENUMS in F#
   type TreeNodeType =
   | Root = 0
   | WebService = 1
   | WebMethod = 2
   | SoapHeader = 3
   | SoapBody = 4
   | Instance = 5
   | TestCase = 6
   | TestInput = 7
   | TestNote = 8
   | TestResult = 9
   | TestExpectedResult = 10
   
   type TreeNodeTag =
    {
      Type : TreeNodeType
      ObjectType : Type option
      ObjectParentType : Type option
      OriginalValue: obj option
      ModifiedValue : obj option    
    }
    
   type InstanceTypes =
      | MyArray of Array
      | MyString of string
      | MyGuid of Guid
      | MyXmlElement of XmlElement
      | MyXmlAttribute of XmlAttribute
      | MyObjectHandle of obj
      | MyNullable of obj
      | MyAbstract of obj
   
   type XmlElemAttrWrapper(a,n,v) = 
      let mutable attrname = a
      let mutable ns = n
      let mutable value =v
      
      member x.Name 
        with get() = attrname
        and set (s:string) = attrname <- s
      member x.NamespaceUri 
        with get() = ns
        and set (s:string) = ns <- s
      member x.Value 
        with get() = value
        and set (s:string) = value <- s
        
      
   type AbstractWrapper =
    {
       DefaultValue : obj
       AbstractType : Type
    } 
    
   module ReflectionOps =
      
    
      let getInnerValueOf (objVal:InstanceTypes) =   
         match objVal with
         | MyArray(myArray) -> box myArray
         | MyString(myStr) -> box myStr
         | MyGuid(myGuid) -> box myGuid
         | MyXmlElement(myElem) ->  box myElem
         | MyXmlAttribute(myAttr) ->  box myAttr
         | MyObjectHandle(myObjHandle) ->  myObjHandle
         | MyNullable(myObjHandle) -> myObjHandle
         | MyAbstract (myUnknownObj) -> myUnknownObj
         
      let rec createInstanceFromType (thisType : Type) : InstanceTypes =
         match thisType with
         | _ when thisType.IsArray -> MyArray(Array.CreateInstance(thisType.GetElementType(),1) )
         | _ when thisType = typeof<string> -> MyString ""
         | _ when thisType = typeof<Guid> -> MyGuid (Guid.NewGuid() )
         | _ when thisType = typeof<XmlElement>  -> 
                 MyXmlElement ((new XmlDocument()).CreateElement("SoapBitsElement"))
         | _ when thisType = typeof<XmlAttribute>  -> 
                MyXmlAttribute ( (new XmlDocument()).CreateAttribute("SoapBitsAttr"))
         | _ when thisType.FullName.StartsWith("System.Nullable") ->
               Nullable.GetUnderlyingType(thisType) |> createInstanceFromType |> getInnerValueOf |> MyNullable
         | _ when thisType.IsAbstract -> 
               let allTypes = thisType.Assembly.GetTypes()
               let concreteType = Array.find (fun (t:Type) -> t.IsSubclassOf(thisType) && not t.IsAbstract) allTypes
               let def = Activator.CreateInstance(concreteType) 
               
               MyAbstract( { DefaultValue = def;AbstractType = thisType } )
                  //MyAbstract(null)
         | _ -> MyObjectHandle (Activator.CreateInstance(thisType) )
      
      
      let getPropertyInfoList (runtimeType:Type) =
         let temp1 = runtimeType.GetProperties( BindingFlags.Public ||| BindingFlags.Instance )
         List.of_array temp1
         
         
      let getFieldInfoList (runtimeType:Type) =
         let temp1 = runtimeType.GetFields(BindingFlags.Public ||| BindingFlags.Instance)
         List.of_array temp1
         
      
      
      
      let processFieldInfo (myObjH:obj) (runtimeType:Type) readType=        
         let fieldInfoList = getFieldInfoList runtimeType
         let rec createChildrenTree (fList : FieldInfo list) (parentObj:obj) (trList:TreeNode list)=
               match fList with
               | head::tail ->
                  
                  let subName = head.Name
                  let subType = head.FieldType
                  let subNode = new TreeNode(subName)
                  let subObj = 
                    match head.GetValue(parentObj) with
                    | o  when o <> null -> o
                    | _ -> createInstanceFromType subType |> getInnerValueOf
                    
                  let clonedAndChild = readType subObj subNode  subName

                  createChildrenTree tail parentObj (List.append trList [clonedAndChild])
               | _ -> trList
            
         createChildrenTree fieldInfoList myObjH []
      
      let processPropInfo (myObjH:obj) (runtimeType:Type) readType=        
         let propInfoList = getPropertyInfoList runtimeType
         let rec createChildrenTree (fList : PropertyInfo list) (parentObj:obj) (trList:TreeNode list)=
               match fList with
               | head::tail ->
                  
                  let subName = head.Name
                  let subType = head.PropertyType
                  let subNode = new TreeNode(subName)
                  let subObj = 
                    match head.GetValue(parentObj, null) with
                    | o  when o <> null -> o
                    | _ -> createInstanceFromType subType |> getInnerValueOf
                    
                  let clonedAndChild = readType subObj subNode  subName

                  createChildrenTree tail parentObj (List.append trList [clonedAndChild])
               | _ -> trList
            
         createChildrenTree propInfoList myObjH []
      
      let setObjectValue (ty:Type) name instance propVal =
         let theInfo = ty.GetProperty(name)
         if theInfo <> null then
           theInfo.SetValue(instance, propVal, null)
           instance
         else
           let temp = ty.GetField(name)
           if temp <> null then
             temp.SetValue(instance, propVal, BindingFlags.Public, null,null)
             instance
           else
             propVal
      
        
      let getVal temp =  (unbox<TreeNodeTag> (temp)).OriginalValue |> Option.get
        
      let createTagExplicit myObjH objType parentOpt= 
         { 
            Type = TreeNodeType.Instance
            ObjectType = Some(objType)
            ObjectParentType = parentOpt
            OriginalValue = Some(myObjH)
            ModifiedValue =  Some(myObjH) 
         }
         
      let createTag myObjH parentOpt= createTagExplicit myObjH (myObjH.GetType()) parentOpt
        
   
    
                 
      let readNode (tr:TreeNode) =
         let methodParams = new ResizeArray<(string*obj)>()
         let test() =           
              let rec readNodeInner (curNode:TreeNode) = 
                 match curNode with 
                 | _ when curNode.Nodes.Count > 0 ->
                    let members = new ResizeArray<(string*obj)>()
                    for node in curNode.Nodes do
                       let name, objVal = readNodeInner node
//                       let valToAdd = 
//                             match unbox<TreeNodeTag> (objVal) with
//                             | t -> 
//                                 match t.ModifiedValue with
//                                 | None -> null
//                                 | Some(modifiedVal) -> modifiedVal
                                 
                       if (node.Parent.Text = "Body" || node.Parent.Text = "Header") &&  node.Parent.Level = 1 then //this is a method parameter
                          methodParams.Add((name, objVal))
                       else
                          members.Add((name, objVal))
                          
                    //Active Patterns are awesome! :)      
                    let (|Array|XmlAttr|Obj|) (ty:Type) =
                       if ty.IsArray then
                         let elemType = ty.GetElementType()
                         Array(  createTag (box(Array.CreateInstance( elemType, members.Count))) (Some(ty.BaseType)) )
                       elif ty =typeof<XmlAttribute> then
                         let temp = new  XmlElemAttrWrapper(curNode.Name, "", "")
                         let tag =
                           let temp = createTag (box temp ) (Some(ty.BaseType))
                           box temp
                         XmlAttr(tag)
                       elif ty = typeof<XmlElemAttrWrapper> then
                         XmlAttr(curNode.Tag)
                       else   
                         Obj(curNode.Tag)
                    
                    let assignMembers ty = 
                      match ty with
                      | Array(temp) ->
                          let theVal =  temp.OriginalValue |> Option.get |> unbox<Array>
                          let mutable c = 0  
                          for (n,v) in members do
                             if (unbox<TreeNodeTag> v).ModifiedValue = None then
                                curNode.Tag <- createTag null None
                             else
                               theVal.SetValue(getVal v, c)                               
                               let tag = createTag (box theVal) temp.ObjectParentType
                               curNode.Tag <- tag
                               
                             c <- c + 1
                      | XmlAttr(temp) ->
                          let theVal = unbox<XmlElemAttrWrapper>( getVal temp)
                          let theAttr = createInstanceFromType (typeof<XmlAttribute>) |> getInnerValueOf |> unbox<XmlAttribute>
                          let docAttr = theAttr.OwnerDocument.CreateAttribute(theVal.Name, theVal.NamespaceUri)
                          docAttr.Value <- theVal.Value
                          curNode.Tag <- createTag (box docAttr) None
                      | Obj(temp) -> 
                          let theVal =  getVal temp
                          for (n,v) in members do
                            if (unbox<TreeNodeTag> v).ModifiedValue = None then
                                curNode.Tag <- createTag null None
                            else
                               let o = getVal v
                               let trTag = unbox<TreeNodeTag> (curNode.Tag)
                               let newVal = setObjectValue (theVal.GetType() ) n theVal o
                               let newValType = newVal.GetType()
                               let tag = createTagExplicit newVal (trTag.ObjectType |> Option.get)  trTag.ObjectParentType
                               curNode.Tag <-tag
                    
                    if members.Count > 0 then    
                      let trTag = unbox<TreeNodeTag> (curNode.Tag)
                      let ty = (trTag.OriginalValue |> Option.get).GetType()
                      assignMembers (ty)   
                                      
                    curNode.Name, curNode.Tag                     
                 | _ -> curNode.Name, curNode.Tag
                
              readNodeInner tr
     
         let n, myVal = test()
         
         let arr = 
            methodParams 
            |> ResizeArray.map (fun (n,objInstance) -> objInstance) 
            |> ResizeArray.map(fun x ->
                 match (unbox<TreeNodeTag> x) with
                 | t ->
                    match t.ModifiedValue with
                    | Some(v) -> v
                    | None -> null )
//                   when t.ModifiedValue = None -> null
//                 | t -> t.OriginalValue |> Option.get )
//                 
              //(unbox<TreeNodeTag> x).OriginalValue |> Option.get)
         arr.ToArray()
      
      let rec readType  (myObjH2:obj) (parentNode:TreeNode) (name:string)=       
         let parentClone:TreeNode = cast  (parentNode.Clone())

         let myObjH,runtimeType, tag =
           if (myObjH2 :? AbstractWrapper) then
              let wrap = unbox<AbstractWrapper> myObjH2
              let tag = createTagExplicit wrap.DefaultValue wrap.AbstractType (Some(wrap.AbstractType.BaseType))
              wrap.DefaultValue , wrap.AbstractType, tag
           else
              let tag = createTag myObjH2 (Some(myObjH2.GetType().BaseType))
              myObjH2, myObjH2.GetType(), tag
         
        // let runtimeType = myObjH.GetType()

              
         parentClone.Tag <- tag
         parentClone.Name <- name
         
         let setAsBottomNode (thisNode:TreeNode) (actualValue:obj) (typ:Type) (name:string)  =
            thisNode.Text <-  formatEq name actualValue
            thisNode
         
         let createTreeRepresentation =
           match runtimeType with
           | _ when runtimeType.ToString().StartsWith("System") && not (runtimeType.IsArray) ->
               setAsBottomNode parentClone myObjH runtimeType name
           | _ when runtimeType.BaseType = typeof<System.Enum> ->
               setAsBottomNode parentClone myObjH runtimeType name
           | _ when runtimeType.IsArray ->               
               let arrayElemType = runtimeType.GetElementType()
               
               //if arrayElemType.ToString() = "XmlAttribute" then               
               if name = arrayElemType.Name then
                 parentClone.Text <- String.Format("{1}[]", (arrayElemType.Name))
               else 
                 parentClone.Text <- String.Format("{0} {1}[]", name, (arrayElemType.Name))
               
               let ar:Array = 
                  if arrayElemType = typeof<XmlAttribute> then
                     //let t = Array.CreateInstance(typeof<XmlElemAttrWrapper>,1 ) |> box
                     let temp =box ( [| new  XmlElemAttrWrapper(arrayElemType.Name, "", "") |] :> Array )
                     //parentClone.Tag <- { Type = TreeNodeType.Instance; OriginalValue = Some(temp); ModifiedValue =  Some(myObjH) }
                     cast temp
                     
                  else
                     cast myObjH
               

               let mutable counter =0
               for arrElemnt in ar do
                    if arrElemnt <> null then
                       let arrNode = new TreeNode(String.Format("{0}[{1}]", arrayElemType.Name,counter))
                       let clonedAndChild:TreeNode = readType arrElemnt arrNode  arrayElemType.Name
                       parentClone.Nodes.Add(clonedAndChild) |> ignore
                       counter <- counter + 1
                    else
                       //will create only one instance
                       let inst = createInstanceFromType arrayElemType |> getInnerValueOf
                       
                       let clonedAndChild:TreeNode = readType inst parentClone  arrayElemType.Name
                       parentClone.Nodes.Add(clonedAndChild) |> ignore
                       
               parentClone
           | _ ->
               let fieldTrList = processFieldInfo  myObjH runtimeType readType
               if List.length fieldTrList > 0 then
                  let createdNodes =  List.to_array ( fieldTrList )
                  parentClone.Nodes.AddRange(createdNodes) |> ignore
            
               let propTrList = processPropInfo myObjH runtimeType readType
               if List.length propTrList > 0 then
                  let createdNodes =  List.to_array ( propTrList )
                  parentClone.Nodes.AddRange(createdNodes) |> ignore   
            
               parentClone
               
         createTreeRepresentation      
         
         
      
        
      
                         
   
      
           