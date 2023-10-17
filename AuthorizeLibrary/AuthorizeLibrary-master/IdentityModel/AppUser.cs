using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthorizeLibrary.IdentityModel
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long? GroupId { get; set; }
        //[ForeignKey("GroupId")]
        //public Group? StudentGroup { get; set; }

        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
