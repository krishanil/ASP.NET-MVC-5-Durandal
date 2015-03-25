namespace WebApplication.BLL.Models.Admin
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
