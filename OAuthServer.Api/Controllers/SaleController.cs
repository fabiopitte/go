using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthServer.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/public")]
    public class SaleController : ApiController
    {
        [Route("sales")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var sales = new Repository<Sale>().Search(new Sale());

            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        //[Route("sale/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var sale = new Repository<Sale>().Get(int.Parse(id));

                sale.Response = new Response { Titulo = "Sucesso", Mensagem = "Venda obtida com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, sale);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter a Venda.");
            }
        }

        [Route("sale/{id}")]
        [HttpGet]
        public HttpResponseMessage GetSaleWithItems(string id)
        {
            try
            {
                var sale = new Repository<Sale>().SearchSaleItemsBySale(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, sale);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter a Venda.");
            }
        }

        [HttpPost]
        [Route("sale")]
        public HttpResponseMessage Post(Sale sale)
        {
            if (null == sale) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao realizar a venda.");

            try
            {
                var s = new Repository<Sale>().Add(sale);

                var novo = new Sale { Id = s.Id, Response = new Response { Titulo = "Sucesso", Mensagem = "Venda realizada com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "OOoops deu um probleminha, verifique os dados e tente novamente.");
            }
        }

        [HttpPut]
        [Route("sale", Name = "sale")]
        public HttpResponseMessage Put([FromBody]Sale sale)
        {
            if (null == sale) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<Sale>().Update(sale);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Venda alterada com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar a Venda.");
            }
        }

        [HttpDelete]
        [Route("sale/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Sale>().Delete(int.Parse(id));

                var sale = new Brand { Response = new Response { Titulo = "Sucesso", Mensagem = "Venda excluida com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, sale);
            }
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Esta venda não pode ser excluida.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir a venda.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}