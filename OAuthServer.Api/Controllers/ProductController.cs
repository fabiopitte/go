using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace OAuthServer.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class ProductController : ApiController
    {
        [Authorize]
        [Route("products")]
        [HttpGet]
        public HttpResponseMessage Search()
        {
            var products = new Repository<Product>().SearchWithInclude(new Product(), "Brand");

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Authorize]
        [HttpPost]
        [Route("PostFormData/{id}")]
        public async Task<HttpResponseMessage> PostFormData(string id)
        {
            if (!Request.Content.IsMimeMultipartContent()) throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Images");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                Product produto = new Repository<Product>().Get(int.Parse(id));

                foreach (MultipartFileData file in provider.FileData)
                {
                    Photo photo = new Photo
                    {
                        ProductId = int.Parse(id),
                        Title = file.Headers.ContentDisposition.FileName,
                        Url = file.LocalFileName
                    };

                    var foto = new Repository<Photo>().Add(photo);

                    produto.Photos.Add(foto);
                }

                new Repository<Product>().Update(produto);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Authorize]
        [Route("product/photos/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTotalPhotos(string id)
        {
            try
            {
                var photos = new Repository<Photo>().Search(int.Parse(id));

                photos.ForEach(p => { p.Url = p.Url.Split(new char[] { '\\' })[5]; });

                return Request.CreateResponse(HttpStatusCode.OK, photos);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter as fotos.");
            }
        }

        [Authorize]
        [Route("product/photo/{url}")]
        [HttpGet]
        public HttpResponseMessage GetPhoto(string url)
        {
            try
            {
                var bmp = Bitmap.FromFile(url);

                var Fs = new FileStream(HostingEnvironment.MapPath("~/Images") + @"\I" + new Guid().ToString() + ".png", FileMode.Create);
                bmp.Save(Fs, ImageFormat.Png);
                bmp.Dispose();

                Image img = Image.FromStream(Fs);
                Fs.Close();
                Fs.Dispose();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);

                HttpResponseMessage response = new HttpResponseMessage();

                response.Content = new ByteArrayContent(ms.ToArray());
                ms.Close();
                ms.Dispose();

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter as fotos.");
            }
        }

        [Route("products/{title}/{code}/{limit}")]
        [HttpGet]
        public HttpResponseMessage Search(string title, string code, int limit)
        {
            var products = new Repository<Product>().Search(new Product { Title = title, Code = code }, limit);

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [Route("product")]
        public HttpResponseMessage Post(Product product)
        {
            if (null == product) return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao incluir o produto.");

            AnularReferencias(product);

            try
            {
                product.InsertDate = System.DateTime.Now;

                var novo = new Repository<Product>().Add(product);

                novo.Response = new Response { Titulo = "Sucesso", Mensagem = "Produto salvo com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.Created, novo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto.");
            }
        }

        [Authorize]
        [HttpPut]
        [Route("product", Name = "product")]
        public HttpResponseMessage Put([FromBody]Product product)
        {
            AnularReferencias(product);

            if (null == product) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var alterado = new Repository<Product>().Update(product);

                alterado.Response = new Response { Titulo = "Sucesso", Mensagem = "Produto alterado com sucesso!" };

                return Request.CreateResponse(HttpStatusCode.OK, alterado);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto.");
            }
        }

        private void AnularReferencias(Product product)
        {
            product.Brand = null; product.Style = null; product.Supplier = null; product.Category = null;
        }

        [Authorize]
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
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Este Produto não pode ser excluido.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o Produto.");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("product/photo/{id}")]
        public HttpResponseMessage DeletePhoto(string id)
        {
            if (id == "0") return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                new Repository<Photo>().Delete(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir a foto.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}