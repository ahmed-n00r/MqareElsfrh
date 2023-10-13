using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class StudentTask:MainModel
    {
        public string Name { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string TaskType { get; set; }
        public DateTime EndDate { get; set; }

        public string RoleId { get; set; }

        public long? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public StudentTask? parentTask { get; set; }

        public List<StudentTask>? studentTasks { get; set; }
        public List<StudentDuty>? taskDuties { get; set; }

    }
}
