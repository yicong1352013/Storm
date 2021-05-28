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
   open System.IO
   open Storm.UI
   open Storm.Types
   open Storm.Types.Configuration
   open Storm.Utilities
   
   [<AbstractClass>]
   type FFormConfig() = 
     inherit FormConfig()
     
     //Local let bindings
     let logTrigger, (logEvent : IEvent<LogMessage>) = Event.create()
     
     abstract GlobalConfig : Configuration.MiscConfigItems
     abstract UserInterfaceConfig : UIConfig
     
     //Implementation of ILog interface
     interface ILog with       
        member x.LogEvt = (logTrigger, logEvent)
      
     
   
   module FFormConfigOps =
      
      let private createFormConfig() =
      
        {
          new FFormConfig() with 
             member x.GlobalConfig
                with get() =
                    let def = ConfigurationImpl.defaultConfig.Clone()
                    def.ProxyServer <- x.tbProxyServer.Text
                    def.ProxyUsername <- x.tbProxyUsername.Text
                    def.ProxyPassword <- x.maskTbPassword.Text
                    def.ProxyDomain <- x.tbDomain.Text
                    def.ProxyUseDefaultCredentials <- x.cbProxyUseDefaultCredentials.Checked
                    def.WebServiceUseDefaultCredentials <- x.cbWSUseDefaultCredentials.Checked
                    //Convert to milliseconds
                    def.SoapCallTimeout <- Convert.ToInt32( x.tbWSSoapCallTimeout.Text ) * 1000
                    
                    def
             member x.UserInterfaceConfig 
                with get() =
                    let def = ConfigurationImpl.defaultUiConfig
                    def.ColorRawSoap <- x.cbColorRawSoap.Checked
                    def.OpenMethodsInTabs <- false // x.cbOpenMethodInTab.Checked
                    def.ShowDetailedException <- x.cbShowDetailedExc.Checked
                    for n in x.chkListBxTop5Ws.CheckedItems do
                        if not (def.MyFavoriteWs.Contains(n.ToString())) then
                           def.MyFavoriteWs.Add(n.ToString())
          
                    def
                    
       
          
             
        }
      
      let thisForm = 
        let temp = createFormConfig()
        temp.gbProxy.Enabled <- false
        temp
      
      let logTrigger, logEvent = (thisForm :> ILog).LogEvt
      
      let loadSettings =
        let hasbeenLoaded = ref false
        fun settingsFile ->
           if not !hasbeenLoaded then 
             let cfgOpt = UIConfig.Read(settingsFile)        
             if cfgOpt <> None then
              let uiCfg = Option.get cfgOpt
              thisForm.cbColorRawSoap.Checked <- uiCfg.ColorRawSoap
              //thisForm.cbOpenMethodInTab.Checked <- false //uiCfg.OpenMethodsInTabs
              thisForm.cbShowDetailedExc.Checked <- uiCfg.ShowDetailedException
              
              thisForm.chkListBxTop5Ws.Items.Clear()
              
              for n in uiCfg.MyFavoriteWs do
                thisForm.chkListBxTop5Ws.Items.Add(n,true) |> ignore
              
              hasbeenLoaded := true
             else //defaults
              ()
      
      let load = loadSettings        
      let file = Path.Combine( GenHelper.exePath, "Storm.settings")  
      
      thisForm.cbUseProxy.CheckedChanged.Add(fun args -> thisForm.gbProxy.Enabled <- thisForm.cbUseProxy.Checked)     
      thisForm.cbProxyUseDefaultCredentials.CheckedChanged.Add(fun args ->
             
             thisForm.tblPanel.Enabled <- not thisForm.cbProxyUseDefaultCredentials.Checked
         )
      
      thisForm.btnAddFaveWs.Click.Add(fun args ->
          let url = thisForm.tbAddFaveWs.Text
          
          if not (thisForm.chkListBxTop5Ws.Items.Contains(url)) then
              thisForm.chkListBxTop5Ws.Items.Add(url, true) |> ignore
      
       )
      thisForm.btnOk.Click.Add(fun _ ->       
           thisForm.DialogResult <- DialogResult.OK
           let uiCfg = new UIConfig()
           uiCfg.ColorRawSoap <- thisForm.cbColorRawSoap.Checked 
           uiCfg.OpenMethodsInTabs <- false //thisForm.cbOpenMethodInTab.Checked
           uiCfg.ShowDetailedException <- thisForm.cbShowDetailedExc.Checked
           
           //Select only the checked services           
           for n in thisForm.chkListBxTop5Ws.CheckedItems do
             if not (uiCfg.MyFavoriteWs.Contains(n.ToString())) then
                uiCfg.MyFavoriteWs.Add(n.ToString())
          
           uiCfg.Save()
         )
         
      thisForm.btnCancel.Click.Add(fun _ ->       
           thisForm.DialogResult <- DialogResult.Cancel
           
           )
      
      
              
      thisForm.Load.Add(fun x -> 
          let uiCfg = new UIConfig()
          load uiCfg.ConfigFile
          
       )
         
      