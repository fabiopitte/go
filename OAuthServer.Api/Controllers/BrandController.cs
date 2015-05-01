using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthServer.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class BrandController : ApiController
    {
        [Route("brandies")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var brandies = new Repository<Brand>().Search(new Brand());

            return Request.CreateResponse(HttpStatusCode.OK, brandies);
        }

        [Route("brandies/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var brand = new Repository<Brand>().Get(int.Parse(id));

                brand.Response = new Response { Titulo = "Sucesso", Mensagem = "Marca obtida com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, brand);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter a Marca.");
            }
        }

        [HttpPost]
        [Route("brand")]
        public HttpResponseMessage Post(Brand brand)
        {
            if (null == brand) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir a Marca.");

            try
            {
                var novo = new Repository<Brand>().Add(brand);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Marca salva com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir a Marca.");
            }
        }

        [HttpPut]
        [Route("brand", Name = "brand")]
        public HttpResponseMessage Put([FromBody]Brand brand)
        {
            if (null == brand) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<Brand>().Update(brand);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Marca alterada com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar a Marca.");
            }
        }

        [HttpDelete]
        [Route("brand/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Brand>().Delete(int.Parse(id));

                var category = new Brand { Response = new Response { Titulo = "Sucesso", Mensagem = "Marca excluida com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Esta Marca não pode ser excluida.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir a Marca.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}