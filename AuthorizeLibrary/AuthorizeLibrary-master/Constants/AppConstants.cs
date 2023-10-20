using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Constants
{
    public static class AppConstants
    {
        //user constants
        public const string defoletPassword = "Pa$$word*123";

        public static bool isRTL = CultureInfo.CurrentCulture.Name.StartsWith("ar");
        public static string colorName = "primary";
        public static string buttonClass { get => $"{colorName} colorClass"; }

        public static string controller = "Home";
        public static string action = "Index";

    }
}
