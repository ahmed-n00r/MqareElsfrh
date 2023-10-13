using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Constants
{
    public class RoleConstants
    {
        //Roles constants
        public static List<string> rolesList() => new List<string> { Admin, Teacher, Student };
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Student = "Student";
    }
}
