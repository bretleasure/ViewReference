using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference.Extensions
{
    internal static class SheetExtensions
    {
        internal static string GetSheetName(this Sheet sheet)
        {
            return sheet.Name.Split(':').First();
        }

        internal static string GetSheetNumber(this Sheet sheet)
        {
            return sheet.Name.Split(':').Last();
        }
    }
}
