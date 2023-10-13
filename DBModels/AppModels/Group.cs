using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.AppModels
{
    public class Group:MainModel
    {
        public string Name { get; set; }

        public List<StudentDuty> groupDuties { get; set; }
    }
}
