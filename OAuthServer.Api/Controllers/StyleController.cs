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
    public class StyleController : ApiController
    {
        [Route("styles")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var styles = new Repository<Style>().Search(new Style());

            return Request.CreateResponse(HttpStatusCode.OK, styles);
        }

        [Route("styles/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var style = new Repository<Style>().Get(int.Parse(id));

                style.Response = new Response { Titulo = "Sucesso", Mensagem = "Estilo obtido com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, style);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter o Estilo.");
            }
        }

        [HttpPost]
        [Route("style")]
        public HttpResponseMessage Post(Style style)
        {
            if (null == style) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o Estilo.");

            try
            {
                var novo = new Repository<Style>().Add(style);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Estilo salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o Estilo.");
            }
        }

        [HttpPut]
        [Route("style", Name = "style")]
        public HttpResponseMessage Put([FromBody]Style style)
        {
            if (null == style) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<Style>().Update(style);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Estilo alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o Estilo.");
            }
        }

        [HttpDelete]
        [Route("style/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Style>().Delete(int.Parse(id));

                var category = new Style { Response = new Response { Titulo = "Sucesso", Mensagem = "Estilo excluido com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Este Estilo não pode ser excluido.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o Estilo.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}