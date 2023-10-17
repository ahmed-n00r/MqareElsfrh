using DBModels.IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBModels.AppModels
{
    public class Group:MainModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<StudentDuty>? groupDuties { get; set; }
        public List<AppUser>? Students { get; set; }

        [NotMapped]
        public int StudentNumber { get { return Students?.Count() ?? 0; } }

    }
}
