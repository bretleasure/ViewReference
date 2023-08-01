using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Windows.Forms;
using ViewReference.Buttons;

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

            AddinGlobal.Automation = new ViewReferenceAutomation();
            
            //Get User Settings

            ViewReferenceTools.GetSavedSettings();

            //Create Event Listener

            ViewReferenceTools.CreateUpdateEventListener();

            try
            {
                if (firstTime)
                {
                    var createButton = new CreateReferencesButton();
                    var removeButton = new RemoveReferencesButton();
                    var configButton = new ConfigureButton();

                    UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;

                    if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
                    {
                        Ribbon ribbon = uiMan.Ribbons["Drawing"];
                        RibbonTab tab = ribbon.RibbonTabs["id_TabPlaceViews"];

                        RibbonPanel panel = tab.RibbonPanels.Add("View Reference", "vr_Panel", Guid.NewGuid().ToString());
                        CommandControls controls = panel.CommandControls;

                        controls.AddButton(createButton.Definition, true, true);
                        controls.AddButton(removeButton.Definition, true, true);
                        controls.AddButton(configButton.Definition, false, true);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the View Reference Addin.");
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
                return AddinGlobal.Automation;
            }
        }

        #endregion

    }
}
