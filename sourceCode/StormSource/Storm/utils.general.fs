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

namespace Storm.Utilities
 
   open System
   open System.Collections
   open System.IO
   open System.Reflection
   open System.Text
   open System.Resources
   open System.Xml
   open System.Xml.Serialization
   open System.Xml.Schema
   open System.Xml.Xsl
   open Microsoft.XmlDiffPatch
   //open System.Windows.Forms.Html
   open System.Runtime.Serialization
   open System.Runtime.Serialization.Formatters.Binary
   open System.Threading
   open System.Drawing
   open System.Diagnostics
   open System.Web.Services.Protocols
   
   module GenHelper =
   
      let formBottomRight (parentW,parentH) (childW, childH)=
         let left = parentW - childW
         let top = parentH - childH
         left,top
         
      let exePath = 
         let temp = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location )
         temp
      
      
      let launchNewStormProcess() =
         let p = 
           let temp = new Process()
           let pInfo = new ProcessStartInfo(Path.Combine(exePath, "storm.exe"))
           temp.StartInfo <- pInfo
           temp
         p.Start() |> ignore
         
            
      let escXmlSpecialChars (str:string) =
        str.Replace("&", "&amp;").Replace("<","&lt;").Replace(">","&gt;").Replace("\"","&quot;").Replace("'","&apos;")
      
      let unescXmlSpecialChars (str:string) =
        str.Replace("&amp;","&").Replace("&lt;","<").Replace("&gt;",">").Replace("&quot;","\"").Replace("&apos;","'")
        
      let utf8stringToStream (str :string) =
        let temp = Encoding.UTF8.GetBytes(str)
        new MemoryStream(temp)
      
      
      let stringToFile (str:string) filename =
        use sw = File.CreateText(filename)
        sw.Write(str)
        sw.Flush()
        
      let fileToString (filename:string) =
        if File.Exists(filename) then
          use strm = File.OpenRead(filename)
          use sr = new StreamReader(strm)
          sr.ReadToEnd()
          
        else
          ""
          
      let fileToByteArray(filename:string) =
        if File.Exists(filename) then
          use fs = File.OpenRead(filename)
          let l = Convert.ToInt32(fs.Length)
          let data = Array.create l (new byte())
          fs.Read (data, 0, data.Length) |> ignore
          data
        else
          Array.create 0 (new byte())
          
      let soapSerialize (objToSerialize:obj) =
        let mapping = (new SoapReflectionImporter()).ImportTypeMapping(objToSerialize.GetType())
        let serializer = new XmlSerializer(mapping)
        use xmlWriter = new XmlTextWriter("./soapSer.xml", Encoding.UTF8)
        serializer.Serialize(xmlWriter, objToSerialize)
        
      
      let getIfNone<'a> optionVal def=
        if optionVal <> None then
          let temp:'a = Option.get optionVal
          true, temp
        else
          false, def
      
      let timedExecute (f: 'a -> 'b) (arg:'a) =
         let start = DateTime.Now         
         let res = 
           try 
             Some(f arg)
           with
           | _ -> None
         res, (DateTime.Now - start).TotalSeconds
      
      let loadXmlFile file =
         try
           if File.Exists(file) then
              let doc= new XmlDocument()
              doc.Load(file)
              (Some(doc),"")
           else
              (None, "File does not exist : " + file)
         with
         | _ as exc -> (None, exc.ToString())
             
              
      let formatElapsed (elapsed:double) =
        String.Format("Elapsed Time : {0}(sec)" , elapsed )
        
      let formatEq left right=
         String.Format("{0} = {1}", left, right)
         
      let deserializeXml<'a> (xdoc:XmlDocument) =
         if xdoc <> null then
            let  taskSerializer = new XmlSerializer(typeof<'a>)
            //let parserContext = new XmlParserContext(null, null, null, XmlSpace.None)
            //using (new XmlTextReader(xdoc.OuterXml, XmlNodeType.Document, parserContext)) (fun (reader:XmlTextReader) ->
            //      let inst = taskSerializer.Deserialize(reader :> XmlReader)
            //      Some( unbox<'a> inst )
               
            //   )

            try 
               //let reader = new XmlTextReader( utf8stringToStream xdoc.OuterXml )
                 // FileStream fs = new FileStream(filename, FileMode.Open);
               let reader = XmlReader.Create(utf8stringToStream xdoc.OuterXml )
               let inst = taskSerializer.Deserialize(reader)
               Some( unbox<'a> inst )
            with 
            | _ as exc -> failwith (exc.ToString())
         else
            None
     
      let serializeObject<'a> (objToSerialize : 'a) =
         match objToSerialize with
         | s when box(s) <> null ->
            use memstrm = new MemoryStream()
            use sr = new StreamReader(memstrm)
            let taskSerializer = new XmlSerializer(typeof<'a>)
            
            taskSerializer.Serialize(memstrm, objToSerialize)
            memstrm.Seek(0L, SeekOrigin.Begin) |> ignore
            sr.ReadToEnd()
         | _ ->
            ""
  
      let binSerializeObject(objToSerialize: obj) filename =
        if File.Exists(filename) then File.Delete(filename)    
        
        use stream = new FileStream( filename, System.IO.FileMode.Create );
        let formatter = new BinaryFormatter();
        formatter.Serialize( stream, objToSerialize );

      
      type DynamicBinder() =
        inherit SerializationBinder()
        let add = new ResizeArray<_>()
        
        member x.AddAssembly(assm:Assembly) =
          add.Add(assm)
        override x.BindToType(assemblyName:string, typeName:string) =
         //System.Diagnostics.Debug.Assert( (typeName <> "TestEnum" ))
          let separator = ",".ToCharArray()
          let toAssm = assemblyName.Split(separator).[0]
          let assemblies = AppDomain.CurrentDomain.GetAssemblies() |> List.of_array
          let rec theType (curAssm: Assembly list) =
            match curAssm with
            | head::tail ->
               if (head.FullName.Split(separator).[0]) = toAssm then
                 head.GetType(typeName)
               else
                 theType tail
            | [] -> add.Item(0).GetType(typeName) //failwith ("Failed to deserialize type : " + typeName )
         
          theType (List.append assemblies (ResizeArray.to_list add) )
      let binDeserializeObject<'a> filename (assm:Assembly) =   
        
        use stream =File.Open(filename, FileMode.Open)
        let formatter=new BinaryFormatter()
        let binder = new DynamicBinder()
        binder.AddAssembly(assm)
        formatter.Binder <- binder
        let c=unbox<'a> (formatter.Deserialize(stream) )
        c

      let createDirectory path =
        if not (Directory.Exists(path)) then
           Directory.CreateDirectory(path) |> ignore
               
      let streamToString (s:Stream) =
         s.Seek(0L, SeekOrigin.Begin) |> ignore
         use sr = new StreamReader(s)
         let temp = sr.ReadToEnd()
         s.Seek(0L, SeekOrigin.Begin) |> ignore
         temp
      
      let cast o = (box o) :?> 'a   
      
      
        
      let xmlPrettyPrint (xmlStr:string) enc = 
        let xdoc = new XmlDocument()
        xdoc.LoadXml(xmlStr)
        use ms = new MemoryStream()
        use w = new XmlTextWriter (ms, enc)
        w.Formatting <- Formatting.Indented
        xdoc.WriteContentTo(w)
        w.Flush()
        ms.Flush()        
        ms.Seek(0L, SeekOrigin.Begin) |> ignore
        
        streamToString (ms :> Stream)
      
      let stringToXmlDoc xmltext =
        try
           let doc = new XmlDocument()
           doc.LoadXml(xmltext)
           Some(doc)
        with
        |_ -> None
        
      let webBrowserXmlText defaultsXslFile (xmlString:string)=
          if File.Exists(defaultsXslFile) then      
            let fs = File.OpenRead(defaultsXslFile)
            let xr = XmlReader.Create(fs)
            let xct = new XslCompiledTransform()
            xct.Load(xr)
           
            let sb = new StringBuilder()
            let xw = XmlWriter.Create(sb)
      
            let docOpt = stringToXmlDoc xmlString 
            if docOpt <> None then             
               xct.Transform( (Option.get docOpt), xw)
               sb.ToString()
            else
               xmlString      
          else
            raise (FileNotFoundException("Missing XSL file",  defaultsXslFile))
      
      
      
      
      let createOrOverwriteFile (rawContent:string) filename =
         match filename with
         | _ when filename <> "" -> 
                use strm = File.Create(filename)
                use sw = new StreamWriter(strm)
                sw.Write(rawContent)
                sw.Flush()
         | _ -> failwith "Filename cannot be empty."  
      
      
      
      let compareXml xml1 xml2 =
         
         let ms = new MemoryStream()
         let xmlw = new XmlTextWriter(ms,Encoding.UTF8)
         
         let reader1 = XmlReader.Create(utf8stringToStream xml1)
         let reader2 = XmlReader.Create(utf8stringToStream xml2)        
         let diff = 
            let temp = new XmlDiff()
            temp.Algorithm <- XmlDiffAlgorithm.Auto
            temp.Options <- XmlDiffOptions.None
            temp
         
         let stat = diff.Compare(reader1, reader2, xmlw)
         stat,ms
      
      let getDiffIfError xml1 xml2 =
         let stat, diffgram = compareXml xml1 xml2
         let text = streamToString diffgram
         let reader1 = XmlReader.Create(utf8stringToStream xml1)
         let readerdiff =  XmlReader.Create(utf8stringToStream text)
            
         if stat then //No Errors
           None
         else
           let diffView = new XmlDiffView()
           diffView.Load(reader1, readerdiff)
           let ms = new MemoryStream()
           let sw = new StreamWriter(ms)
           sw.Write("<html><body><table style=\"font: xx-small Verdana, Georgia, serif;\" width='65%'>")
           sw.Write("<tr><td style='background-color: black; color: white; font: 11pt' align='center'>Expected response</td><td style='background-color: gray; color: white; font: 11pt' align='center'>Actual Response</td></tr>")
           sw.Flush()
           diffView.GetHtml( sw )
           sw.Flush()
           sw.Write("</table></body></html>")
           sw.Flush()
           let html = streamToString ms
           
           Some(html)
           
             
      //Not a very functional piece of code.:( Good thing its not being used
      let readWholeStream (stream:Stream) length=
          let getInitLength =
                    match length with
                    | _ when length < 1L -> 323768L //32K
                    | _ -> length
                  
          let initLength = getInitLength
          let mutable read = 0
          let mutable buffer =[| new byte() |]
          let mutable chunk = -11
          let mutable stop = false
          
          while (not stop) && chunk <> -11 do
                     chunk <- stream.Read(buffer, read, buffer.Length-read)   
                     if read = buffer.Length then
                        let nextByte = stream.ReadByte()
                        if nextByte = -1 then
                          stop <- true
                        else
                             let newBuffer = Array.create(buffer.Length * 2) (new byte())
                             Array.Copy(buffer, newBuffer, buffer.Length)
                             let asByte : byte = Convert.ToByte(nextByte)
                             newBuffer.[read] <- asByte
                             buffer <- newBuffer
                             read <- read + 1

          let ret = Array.create(read) (new byte())
          Array.Copy(buffer, ret, read)
          ret          
              
              

        
             
                 
                    
      
   
      