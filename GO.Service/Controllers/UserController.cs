using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GO.Service.Controllers
{
    [RoutePrefix("api/v1/public")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class USerController : ApiController
    {
        [Route("users")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var users = new Repository<User>().Search(new User());

            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [Route("users/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var user = new Repository<User>().Get(int.Parse(id));

                user.Response = new Response { Titulo = "Sucesso", Mensagem = "Usuario salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter o usuario.");
            }
        }

        [HttpPost]
        [Route("user")]
        public HttpResponseMessage Post(User user)
        {
            if (null == user) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o usuario.");

            try
            {
                var novo = new Repository<User>().Add(user);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Usuario salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o usuario.");
            }
        }

        [HttpPut]
        [Route("user", Name = "user")]
        public HttpResponseMessage Put([FromBody]User user)
        {
            if (null == user) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<User>().Update(user);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Usuario alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o usuario.");
            }
        }

        [HttpDelete]
        [Route("user/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<User>().Delete(int.Parse(id));

                var category = new User { Response = new Response { Titulo = "Sucesso", Mensagem = "Usuario excluido com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o usuario.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}