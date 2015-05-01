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
    public class CategoryController : ApiController
    {
        [Route("categories")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var categories = new Repository<Category>().Search(new Category());

            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        [Route("categories/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var category = new Repository<Category>().Get(int.Parse(id));

                category.Response = new Response { Titulo = "Sucesso", Mensagem = "Categoria salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter a Categoria.");
            }
        }

        [HttpPost]
        [Route("category")]
        public HttpResponseMessage Post(Category category)
        {
            if (null == category) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao cadastrar a Categoria.");

            try
            {
                var novo = new Repository<Category>().Add(category);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Categoria cadastrada com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao cadastrar a Categoria.");
            }
        }

        [HttpPut]
        [Route("category", Name = "category")]
        public HttpResponseMessage Put([FromBody]Category category)
        {
            if (null == category) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<Category>().Update(category);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Categoria alterada com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar a Categoria.");
            }
        }

        [HttpDelete]
        [Route("category/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Category>().Delete(int.Parse(id));

                var category = new Category { Response = new Response { Titulo = "Sucesso", Mensagem = "Categoria excluida com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Esta Categoria não pode ser excluida.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir a Categoria.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}