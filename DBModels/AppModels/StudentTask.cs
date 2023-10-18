using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBModels.AppModels
{
    public class StudentTask:MainModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "TaskStatus")]
        public DBModels.AppConstants.TaskStatus TaskStatus { get; set; }
        [Display(Name = "TaskType")]
        public string TaskType { get; set; }
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }
        [Display(Name = "RoleName")]
        public string RoleId { get; set; }

        [Display(Name = "TaskId")]
        public long? TaskId { get; set; }
        [ForeignKey("TaskId")]
        [Display(Name = "TaskId")]
        public StudentTask? parentTask { get; set; }

        public List<StudentTask>? studentTasks { get; set; }
        public List<StudentDuty>? taskDuties { get; set; }

    }
}
