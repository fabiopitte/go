using System.Web.Http;

namespace OAuthServer.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class ValuesController : ApiController
    {
        [Authorize()]
        [Route("values")]
        public string Get() 
        {
            return User.Identity.Name;
        }
    }
}