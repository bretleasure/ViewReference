using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Windows.Forms;
using CAP.Utilities;

namespace CAP.Apps.ViewReference
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("35678d1f-aeb7-4a22-9fef-624c83e66007")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        //private Inventor.Application m_inventorApplication;

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            // Initialize AddIn members.
            AddinGlobal.InventorApp = addInSiteObject.Application;

            //Create App Folder if it doesnt exist
            if (!System.IO.Directory.Exists(AddinGlobal.AppFolder))
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(AddinGlobal.AppFolder);
                di.Attributes = FileAttributes.Hidden;
            }

            //Get User Settings
            ViewRefTools.Get_SavedSettings();

            try
            {
                Icon CreateUpdate = new Icon(this.GetType(), "Resources.capico.ico");
                Icon CreateUpdate_sm = new Icon(CreateUpdate, 16, 16);
                InventorButton btn_CreateUpdate = new InventorButton("Create/Update", "vr_CreateUpdate", "Create/Update View References", "Create/Update View References in this document.", CreateUpdate, CreateUpdate_sm);
                btn_CreateUpdate.Execute = ViewRef_ButtonEvents.CreateUpdate_References;

                Icon RemoveRef = new Icon(this.GetType(), "Resources.capico.ico");
                Icon RemoveRef_sm = new Icon(RemoveRef, 16, 16);
                InventorButton btn_Remove = new InventorButton("Remove", "vr_Remove", "Remove View References", "Remove View References in this document.", RemoveRef, RemoveRef_sm);
                btn_Remove.Execute = ViewRef_ButtonEvents.Remove_References;

                Icon Configure = new Icon(this.GetType(), "Resources.gear.ico");
                Icon Configure_sm = new Icon(Configure, 16, 16);
                InventorButton btn_Configure = new InventorButton("Configure", "vr_Config", "Configure View Reference", "Select Options for View Reference.", Configure, Configure_sm);
                btn_Configure.Execute = ViewRef_ButtonEvents.ShowConfigForm;

                if (firstTime)
                {
                    UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;

                    if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
                    {
                        Ribbon ribbon = uiMan.Ribbons["Drawing"];
                        RibbonTab tab = ribbon.RibbonTabs["id_TabPlaceViews"];

                        RibbonPanel panel = tab.RibbonPanels.Add("View Reference", "vr_Panel", Guid.NewGuid().ToString());
                        CommandControls controls = panel.CommandControls;

                        controls.AddButton(btn_CreateUpdate.ButtonDef(), true, true);
                        controls.AddButton(btn_Remove.ButtonDef(), true, true);
                        controls.AddButton(btn_Configure.ButtonDef(), false, true);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }       


        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            AddinGlobal.InventorApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion

    }
}
