namespace WebApplication.BLL.Models.Account.ViewModels
{
    public class UserInfoModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }

        public string UserRoles { get; set; }
    }
}
