using System.ComponentModel.DataAnnotations;

namespace WebApplication.BLL.Models.Account.BindingModels
{
    public class RegisterExternalModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
