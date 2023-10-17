using DBModels.AppConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.ViewModel
{
    public class ModelData
    {
        public string Name { get; set; }
        public string MainAction { get; set; }
        public ModelActivationStatus IsActive { get; set; }
        public string Icon { get; set; }
        public int Index { get; set; }
        public string VeiwName { get; set; }
    }
}
