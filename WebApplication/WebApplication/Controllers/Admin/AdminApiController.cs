using System.Collections.Generic;
using System.Web.Http;
using WebApplication.BLL.Managers.Admin;
using WebApplication.BLL.Models.Admin;

namespace WebApplication.Controllers.Admin
{
    [Authorize(Roles = "Administrators")]
    [RoutePrefix("api/Admin")]
    public class AdminApiController : ApiController
    {
        private IAdminManager Manager { get; set; }

        public AdminApiController(IAdminManager manager)
        {
            Manager = manager;
        }

        // GET api/Admin/Users
        [Route("Users")]
        [HttpGet]
        public IEnumerable<UserModel> Users()
        {
            return Manager.Users();
        }
    }
}
