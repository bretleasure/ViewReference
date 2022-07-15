using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Windows.Forms;
using iAD.Utilities;
using Microsoft.Extensions.Logging;

namespace ViewReference
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

            // Initialize AddIn members.
            AddinGlobal.InventorApp = addInSiteObject.Application;

            AddinGlobal.Logger = Logging.GetLogger<StandardAddInServer>();

            AddinGlobal.Logger.LogInformation("Initializing View Reference Addin");

            //if (!LicTools.CheckForValidUser(AddinGlobal.InventorApp, "View Reference", AddinGlobal.AppId))
            //{
            //    AddinGlobal.Logger.LogWarning("Invalid License");
            //    return;
            //}
            
            //Get User Settings
            AddinGlobal.Logger.LogInformation("Retrieving Settings");
            ViewReference_Tools.Get_SavedSettings();

            //Create Event Listener
            AddinGlobal.Logger.LogInformation("Creating Event Listener");
            ViewReference_Tools.CreateUpdateEventListener();

            try
            {
                AddinGlobal.Logger.LogInformation("Creating Ribbon Buttons");

                //Make sure Icons are marked as Embedded Resource in the properties

                Icon CreateUpdate_Icon = new Icon(this.GetType(), "Resources.ViewRef-Add.ico");
                 Icon CreateUpdate_Icon_sm = new Icon(CreateUpdate_Icon, 16, 16);

                InventorButton CreateUpdate_Btn = new InventorButton("Create /\rUpdate", "vr_CreateUpdate", "Create/Update View References", "Create/Update View References in this document.", CreateUpdate_Icon, CreateUpdate_Icon_sm);
                CreateUpdate_Btn.Execute = ViewReference_ButtonEvents.CreateUpdate_References;

                Icon Remove_Icon = new Icon(this.GetType(), "Resources.ViewRef-Remove.ico");
                Icon Remove_Icon_sm = new Icon(Remove_Icon, 16, 16);
                InventorButton Remove_Btn = new InventorButton("Remove", "vr_Remove", "Remove View References", "Remove View References in this document.", Remove_Icon, Remove_Icon_sm);
                Remove_Btn.Execute = ViewReference_ButtonEvents.Remove_References;

                Icon Config_Icon = new Icon(this.GetType(), "Resources.gear.ico");
                Icon Config_Icon_sm = new Icon(Config_Icon, 16, 16);
                InventorButton Config_Btn = new InventorButton("Configure", "vr_Config", "Configure View Reference", "Select Options for View Reference.", Config_Icon, Config_Icon_sm);
                Config_Btn.Execute = ViewReference_ButtonEvents.ShowConfigForm;


                if (firstTime)
                {
                    UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;

                    if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
                    {
                        Ribbon ribbon = uiMan.Ribbons["Drawing"];
                        RibbonTab tab = ribbon.RibbonTabs["id_TabPlaceViews"];

                        RibbonPanel panel = tab.RibbonPanels.Add("View Reference", "vr_Panel", Guid.NewGuid().ToString());
                        CommandControls controls = panel.CommandControls;

                        AddinGlobal.Logger.LogInformation("Adding buttons to the ribbon");

                        controls.AddButton(CreateUpdate_Btn.ButtonDef(), true, true);
                        controls.AddButton(Remove_Btn.ButtonDef(), true, true);
                        controls.AddButton(Config_Btn.ButtonDef(), false, true);

                    }
                }
            }
            catch (Exception ex)
            {
                AddinGlobal.Logger.LogInformation(ex, "Could not load addin");
                MessageBox.Show("Could not load the View Reference Addin. Check log file for details");
            }


        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated


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

                return null;
            }
        }

        #endregion

    }
}
