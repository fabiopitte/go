using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace GO.WebStore
{
    public class UtilController : ApiController
    {
        [Route("product/photos/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTotalPhotos(string id)
        {
            try
            {
                var photos = new GO.Infra.SqlServer.Repository<GO.Domain.Photo>().Search(int.Parse(id));

                photos.ForEach(p =>
                {
                    p.Url = p.Url.Split(new char[] { '\\' })[5];

                    var enderecoFoto = HostingEnvironment.MapPath("~/Images") + @"\" + p.Url + ".jpg";

                    if (!File.Exists(enderecoFoto)) File.WriteAllBytes(enderecoFoto, p.File);

                    //seta o novo endereco
                    p.Url = "Images/" + p.Url + ".jpg";
                });

                return Request.CreateResponse(HttpStatusCode.OK, photos);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter as fotos.");
            }
        }
    }
}