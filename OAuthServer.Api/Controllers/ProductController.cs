using GO.Domain;
//using GO.Infra.MongoDb;
using GO.Infra.SqlServer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace OAuthServer.Api.Controllers
{
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

        [HttpPost]
        [Route("PostFormData/{id}")]
        public async Task<HttpResponseMessage> PostFormData(string id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Images");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                Product produto = new Repository<Product>().Get(int.Parse(id));

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Photo photo = new Photo
                    {
                        Title = file.Headers.ContentDisposition.FileName,
                        Url = file.LocalFileName
                    };
                    
                    produto.Photos.Add(photo);

                    var pro = new Repository<Product>().Update(produto);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("product/photos/{id}")]
        [HttpGet]
        public HttpResponseMessage GetPhotos(string id)
        {
            try
            {
                string Imagespath = HttpContext.Current.Server.MapPath("~/Images/");
                string SitePath = HttpContext.Current.Server.MapPath("~");
                var Files = (from file in Directory.GetFiles(Imagespath) select new { image = file.Replace(SitePath, "~") }).Take(10);
                //ImageGrid.DataSource = Files.ToList();

                var photos = new Repository<Photo>().Search(int.Parse(id));

                if (photos.Count > 0)
                {
                    #region
                    var bmp = Bitmap.FromFile(photos.FirstOrDefault().Url);
                    //3
                    var Fs = new FileStream(HostingEnvironment.MapPath("~/Images") + @"\I" + id + ".png", FileMode.Create);
                    bmp.Save(Fs, ImageFormat.Png);
                    bmp.Dispose();

                    //4
                    Image img = Image.FromStream(Fs);
                    Fs.Close();
                    Fs.Dispose();

                    //5
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Png);

                    HttpResponseMessage response = new HttpResponseMessage();

                    //6
                    response.Content = new ByteArrayContent(ms.ToArray());
                    ms.Close();
                    ms.Dispose();

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    response.StatusCode = HttpStatusCode.OK;
                    return response;

                    #endregion
                }

                return Request.CreateResponse(HttpStatusCode.Created, photos);
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

        [HttpPut]
        [Route("product", Name = "product")]
        public HttpResponseMessage Put([FromBody]Product product)
        {
            if (null == product) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                Validar(product);

                product.InsertDate = null;

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
            catch (System.Exception ex)
            {
                if (ex.HResult.Equals(-2146233087))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Este Produto não pode ser excluido.");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o Produto.");
            }
        }

        private static void Validar(Product product)
        {
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