using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
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
        [Route("products")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var products = new Repository<Product>().Search(new Product());

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("products/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var product = new Repository<Product>().Get(int.Parse(id));

                product.Response = new Response { Titulo = "Sucesso", Mensagem = "Produto obtido com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter o produto.");
            }
        }

        [HttpPost]
        [Route("product")]
        public HttpResponseMessage Post(Product product)
        {
            if (null == product) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o produto.");

            try
            {
                Validar(product);

                var novo = new Repository<Product>().Add(product);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Produto salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto.");
            }
        }

        [HttpPut]
        [Route("product", Name = "product")]
        public HttpResponseMessage Put([FromBody]Product product)
        {
            if (null == product) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                Validar(product);

                var alterado = new Repository<Product>().Update(product);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Produto alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto.");
            }
        }

        [HttpDelete]
        [Route("product/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Product>().Delete(int.Parse(id));

                var product = new Product { Response = new Response { Titulo = "Sucesso", Mensagem = "Produto excluido com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o produto.");
            }
        }

        private static void Validar(Product product)
        {
            product.PhotoId = product.PhotoId == 0 ? null : product.PhotoId;
            product.SupplierId = product.SupplierId == 0 ? null : product.SupplierId;
            product.BrandId = product.BrandId == 0 ? null : product.BrandId;
            product.CategoryId = product.CategoryId == 0 ? null : product.CategoryId;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}