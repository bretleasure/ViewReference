﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference
{
	public static class InventorApplicationExtensions
	{
        public static dynamic GetViewReferenceAddin(this Application inventor)
        {
            var addin = inventor.ApplicationAddIns.ItemById["{35678d1f-aeb7-4a22-9fef-624c83e66007}"];

            if (addin != null)
            {
                addin.Activate();

                return addin.Automation;
            }
            else
                return null;
        }
    }
}
