using DBModels.AppConstants;
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
    public class StudentDuty: MainModel
    {
        [Display(Name = "DutyStatus")]
        public DutyStatus DutyStatus { get; set; }

        [Display(Name = "UserName")]
        public string? UserId { get; set; }

        public long TaskId { get; set; }
        [ForeignKey("TaskId")]
        [Display(Name = "Task")]
        public StudentTask? task { get; set; }

        public long GroupId { get; set; }
        [ForeignKey("GroupId")]
        [Display(Name = "Group")]
        public Group? group { get; set; }
    }
}
