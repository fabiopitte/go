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
    public class SupplierController : ApiController
    {
        [Route("suppliers")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var suppliers = new Repository<Supplier>().Search(new Supplier());

            return Request.CreateResponse(HttpStatusCode.OK, suppliers);
        }

        [Route("suppliers/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var supplier = new Repository<Supplier>().Get(int.Parse(id));

                supplier.Response = new Response { Titulo = "Sucesso", Mensagem = "Fornecedor salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, supplier);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter o produto.");
            }
        }

        [HttpPost]
        [Route("supplier")]
        public HttpResponseMessage Post(Supplier supplier)
        {
            if (null == supplier) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o produto.");

            try
            {
                var novoSupplier = new Repository<Supplier>().Add(supplier);

                novoSupplier.Response = new Response { Titulo = "Sucesso", Mensagem = "Fornecedor salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novoSupplier);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto.");
            }
        }

        [HttpPut]
        [Route("supplier", Name = "supplier")]
        public HttpResponseMessage Put([FromBody]Supplier supplier)
        {
            if (null == supplier) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var supplierAlterado = new Repository<Supplier>().Update(supplier);

                supplierAlterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Fornecedor alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, supplierAlterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto.");
            }
        }

        [HttpDelete]
        [Route("supplier/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Supplier>().Delete(int.Parse(id));

                var supplier = new Supplier { Response = new Response { Titulo = "Sucesso", Mensagem = "Fornecedor excluido com sucesso!" } };

                return Request.CreateResponse(HttpStatusCode.OK, supplier);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o produto.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}