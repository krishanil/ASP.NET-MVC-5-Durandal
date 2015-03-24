using System.Collections.Generic;

namespace WebApplication.BLL.Models.Account.ViewModels
{
    public class ManageInfoModel
    {
        public string LocalLoginProvider { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoModel> Logins { get; set; }

        public IEnumerable<ExternalLoginModel> ExternalLoginProviders { get; set; }
    }
}
