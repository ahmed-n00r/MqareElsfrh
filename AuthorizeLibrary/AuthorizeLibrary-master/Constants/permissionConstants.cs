using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Constants
{
    public static class permissionConstants
    {
        public const string Permission = "Permission";
        public const string View = "View";
        public const string Create = "Create";
        public const string Edit = "Edit";
        public const string Delete = "Delete";

        public static List<string> ActionList() => new List<string> { View, Create, Edit, Delete };
        public static List<string> PermissionsList(string modelNmae)
        {
            var list = new List<string>();
            foreach (var item in ActionList())
                list.Add($"{Permission}.{modelNmae}.{item}");
            return list;
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            //var modules = ModelConstants.ModelList();

            //foreach (var module in modules)
            //    allPermissions.AddRange(PermissionsList(module.ToString()));

            return allPermissions;
        }
    }
}
