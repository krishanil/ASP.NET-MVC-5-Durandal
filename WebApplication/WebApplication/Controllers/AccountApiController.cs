using System.Web.Http;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountApiController : ApiController
    {
        // GET: api/AccountApi
        [AllowAnonymous]
        public bool Get()
        {
            return User.Identity.IsAuthenticated;
        }

        // GET: api/AccountApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AccountApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AccountApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AccountApi/5
        public void Delete(int id)
        {
        }
    }
}
