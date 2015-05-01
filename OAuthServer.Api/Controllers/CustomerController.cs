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
    public class CustomerController : ApiController
    {
        [Route("customers")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var customers = new Repository<Customer>().Search(new Customer());

            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [Route("customers/{name}/{limit}")]
        [HttpGet]
        public HttpResponseMessage Search(string name, int limit)
        {
            var customers = new Repository<Customer>().Search(new Customer { Nome = name }, limit);

            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [Route("customers/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var customer = new Repository<Customer>().Get(int.Parse(id));

                customer.Response = new Response { Titulo = "Sucesso", Mensagem = "Cliente salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter o cliente.");
            }
        }

        [HttpPost]
        [Route("customer")]
        public HttpResponseMessage Post(Customer customers)
        {
            if (null == customers) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o cliente.");

            try
            {
                var novo = new Repository<Customer>().Add(customers);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Cliente salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o cliente.");
            }
        }

        [HttpPut]
        [Route("customer", Name = "customer")]
        public HttpResponseMessage Put([FromBody]Customer customer)
        {
            if (null == customer) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Address>().Update(customer.Endereco);

                var alterado = new Repository<Customer>().Update(customer);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Cliente alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o cliente.");
            }
        }

        [HttpDelete]
        [Route("customer/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Customer>().Delete(int.Parse(id));

                var customer = new Customer { Response = new Response { Titulo = "Sucesso", Mensagem = "Cliente excluido com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Este Cliente não pode ser excluido.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o Cliente.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}