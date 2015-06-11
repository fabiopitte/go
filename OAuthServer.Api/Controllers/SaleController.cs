using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System;

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
                //primeiro realiza a venda
                var saled = new Repository<Sale>().Add(sale);

                var novo = new Sale { Id = saled.Id, Response = new Response { Titulo = "Sucesso", Mensagem = "Venda realizada com sucesso!" } };

                //percorre os itens da venda
                saled.Itens.ToList().ForEach(i =>
                {
                    //obtem o produto
                    var product = new Repository<Product>().Get(i.ProductId);

                    //realiza a subtracao do item
                    var quantity = product.Quantity - i.Quantity;

                    product.Quantity = quantity;

                    //segundo retira a quantidade vendida do estoque
                    new Repository<Product>().Update(product);
                });

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "OOoops deu um probleminha, verifique os dados e tente novamente." + ex.InnerException.ToString());
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

        [HttpPost]
        [Route("sale/dispatch")]
        public HttpResponseMessage RealizarDevolucao(List<Sale> sale)
        {
            if (null == sale) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao realizar a devolucao dos produtos.");

            try
            {
                sale.ToList().ForEach(s =>
                {
                    //percorre os itens da venda e realiza a devolucao dos produtos
                    s.Itens.ToList().ForEach(i =>
                    {
                        //obtem o produto
                        var product = new Repository<Product>().Get(i.ProductId);

                        //realiza a soma do item
                        var quantity = product.Quantity + i.Quantity;

                        product.Quantity = quantity;

                        //segundo adiciona a quantidade que estava locada no estoque
                        new Repository<Product>().Update(product);

                        //obtem a venda do cliente para comparativo
                        var ItemDoCliente = new Repository<Item>().Get(i.Id);

                        //realiza a devolucao do produto nos itens do cliente
                        if (ItemDoCliente.Quantity == i.Quantity)
                        {
                            ItemDoCliente.DateDispatched = System.DateTime.Now;
                            ItemDoCliente.ProductDispatched = true;
                        }
                        else
                        {
                            ItemDoCliente.Quantity = (ItemDoCliente.Quantity - i.Quantity);
                        }

                        //Realiza a baixa do item 
                        new Repository<Item>().Update(ItemDoCliente);
                    });
                });

                var venda = new Sale { Response = new Response { Titulo = "Sucesso", Mensagem = "Baixa dos itens realizada com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.Created, venda);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "OOoops ocorreu um probleminha, verifique os dados e tente novamente.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}