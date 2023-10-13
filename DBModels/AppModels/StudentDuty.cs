using DBModels.AppConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class StudentDuty: MainModel
    {
        public DutyStatus DutyStatus { get; set; }

        public string UserId { get; set; }

        public long TaskId { get; set; }
        [ForeignKey("TaskId")]
        public StudentTask task { get; set; }

        public long GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group group { get; set; }
    }
}
