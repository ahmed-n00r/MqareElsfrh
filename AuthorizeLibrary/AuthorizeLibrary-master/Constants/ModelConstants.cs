using DBModels.AppConstants;
using DBModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeLibrary.Constants
{
    public class ModelConstants
    {
        public static ModelData GroupModel { get => new() { IsActive = ModelActivationStatus.NotActive, Name = "Groups", MainAction = "Index", Icon = "ni ni-calendar-grid-58 text-warning", Index = 2, VeiwName = "Group" };  }

        public static ModelData HomeModel { get => new() { IsActive = ModelActivationStatus.Active, Name = "Home", MainAction = "Index", Icon = "ni ni-tv-2 text-primary", Index = 1, VeiwName = "Dashboard" }; }

        public static ModelData StudentTaskModel { get => new() { IsActive = ModelActivationStatus.NotActive, Name = "StudentTasks", MainAction = "Index", Icon = "ni ni-paper-diploma text-info", Index = 2, VeiwName = "StudentTask" }; }

        //public static ModelData StudentDutyModel { get => new() { IsActive = ModelActivationStatus.NotActive, Name = "Group", MainAction = "Index", Icon = "" };  }

        //public static ModelData StudentModel { get => new() { IsActive = ModelActivationStatus.NotActive, Name = "Group", MainAction = "Index", Icon = "" }; }

        public static List<ModelData> ModelList = new List<ModelData> {
                GroupModel,
                HomeModel,
                StudentTaskModel,
                //StudentDutyModel,
                //StudentModel
            }.OrderBy(k => k.Index).ToList();

        public static void setAllNotActive(ModelData model)
        {
            foreach(var item in ModelList)
            {
                item.IsActive = item.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)
                    ?  ModelActivationStatus.Active
                    : ModelActivationStatus.NotActive;
            }
        }
    }
}
