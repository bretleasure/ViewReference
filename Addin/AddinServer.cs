﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;
using Path = System.IO.Path;
using ViewReference.Buttons;

namespace ViewReference
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [Guid("35678d1f-aeb7-4a22-9fef-624c83e66007")]
    public class AddinServer : Inventor.ApplicationAddInServer
    {
        static AddinServer()
        {
            //Resolve assemblies to local directory if they are not found
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name).Name;
                var assemblyPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyName + ".dll");
                if (System.IO.File.Exists(assemblyPath))
                {
                    return Assembly.LoadFrom(assemblyPath);
                }
                return null;
            };
        }
        
        // The Inventor application instance
        public static Inventor.Application InventorApp;

        public static Guid AddinGuid = new("35678d1f-aeb7-4a22-9fef-624c83e66007");

        List<InventorButton> _buttons;

        internal static string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");
        internal static ViewReferenceAddinSettings Settings { get; set; }
        internal static ViewReferenceAutomation AppAutomation { get; set; }
        internal static string AttributeSetName { get; } = "ViewReference-v4";
        

        #region ApplicationAddInServer Members

        /// <summary>
        /// This method is called by Inventor when it loads the addin.
        /// The AddInSiteObject provides access to the Inventor Application object.
        /// The FirstTime flag indicates if the addin is loaded for the first time.
        /// </summary>
        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            InventorApp = addInSiteObject.Application;
            InventorApp.ApplicationEvents.OnApplicationOptionChange += UpdateButtons;

            AppAutomation = new ViewReferenceAutomation();
            
            //Get User Settings
            ViewReferenceTools.GetSavedSettings();

            //Create Event Listener
            ViewReferenceTools.CreateUpdateEventListener();

            try
            {
                // If the addin is loaded for the first time, initialize the UI components
                if (firstTime)
                {
                    InitializeUIComponents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load Addin.");
            }
        }

        /// <summary>
        /// Initializes the UI components of the addin.
        /// </summary>
        private void InitializeUIComponents()
        {
            _buttons = Assembly.GetAssembly(typeof(InventorButton)).GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(InventorButton)))
                .Select(Activator.CreateInstance)
                .Cast<InventorButton>()
                .Where(button => button.Enabled)
                .OrderBy(button => button.SequenceNumber)
                .ToList();

            _buttons.ForEach(b => b.Initialize());
        }

        /// <summary>
        /// Updates the buttons when the application options change.
        /// </summary>
        private void UpdateButtons(EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kAfter)
            {
                _buttons.ForEach(b => b.Dispose());

                InitializeUIComponents();

                handlingCode = HandlingCodeEnum.kEventHandled;
            }

            handlingCode = HandlingCodeEnum.kEventNotHandled;
        }

        /// <summary>
        /// This method is called by Inventor when the AddIn is unloaded.
        /// The AddIn will be unloaded either manually by the user or
        /// when the Inventor session is terminated.
        /// </summary>
        public void Deactivate()
        {
            InventorApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// This method is now obsolete, you should use the
        /// ControlDefinition functionality for implementing commands.
        /// </summary>
        public void ExecuteCommand(int commandID)
        {
        }

        /// <summary>
        /// This property is provided to allow the AddIn to expose an API
        /// of its own to other programs. Typically, this  would be done by
        /// implementing the AddIn's API interface in a class and returning
        /// that class object through this property.
        /// </summary>
        public object Automation
        {
            get
            {
                return AppAutomation;
            }
        }

        #endregion
    }
}