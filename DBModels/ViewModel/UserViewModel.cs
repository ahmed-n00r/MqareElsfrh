using System.ComponentModel.DataAnnotations;

namespace DBModels.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }
    }
}
