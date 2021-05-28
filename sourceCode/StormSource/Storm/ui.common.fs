#light
namespace Storm.Types
   open System
   open System.Drawing
   open System.Drawing.Design
   open System.Windows.Forms
   open System.Text
   open System.IO
   open Microsoft.XmlDiffPatch
   //open System.Windows.Forms.Html
   open Storm.UI
   open Storm.Types
   open Storm.Utilities.GenHelper
   open Storm.Types.Configuration
   open Storm.Types.Configuration.ConfigReaderImpl
   open System.ComponentModel
   
   
   type GradientTheme = {GradientHighColor : Color ; GradientLowColor : Color; RenderMode : string}
   
   module UICommon =
   
       let theme =
         let gh,gl,r = readAppSettings "GradientHighColor", readAppSettings "GradientLowColor", readAppSettings "GradientRenderMode"
         match gh,gl,r with
         | None,_,_ | _,None,_ | _,_,None | None,None,None-> {GradientHighColor = SystemColors.Window;  GradientLowColor = SystemColors.GradientActiveCaption; RenderMode = "Glass"}
         | Some(grHigh), Some(grLow), Some(renderMode) -> {GradientHighColor = Color.FromName grHigh;  GradientLowColor =Color.FromName grLow; RenderMode = renderMode}
       
       let showNotifier (x:UserControl) msg =
         let screenPoint = x.ParentForm.PointToScreen(x.Location)           
         let noty msg2 = 
           let temp = new Notifier()
           temp.DisposeWhenFinished <- true;
           temp.Message <- msg2
           temp.Location <- new Point(screenPoint.X + (x.ParentForm.Width-temp.Width-15) , screenPoint.Y + (x.ParentForm.Height-temp.Height-55) )
           temp
            //position at the bottom right corner of the parent form
            
         (noty msg).Show()
      