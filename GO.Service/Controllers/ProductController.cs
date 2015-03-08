using GO.Domain;
using GO.Infra;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GO.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/public")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("product")]
        public HttpResponseMessage Search()
        {
            var products = new Repository<Product>().Search(new Product());

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [HttpPost]
        [Route("product")]
        public Product Add(Product product)
        {
            return new Repository<Product>().Add(product);
        }
    }
}