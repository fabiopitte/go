using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

namespace GO.GoStore
{
    public class UtilController : ApiController
    {
        //[Authorize]
        //[Route("product/photo/{url}")]
        [HttpGet]
        public HttpResponseMessage GetPhoto(string url)
        {
            try
            {
                var UrlBase = "C:/Projetos/go/OAuthServer.Api/Images/";
                var UrlBaseVirtual = "http://localhost:60629/Images/";

                var bmp = Bitmap.FromFile(UrlBase + url);
                //3
                var Fs = new FileStream(HostingEnvironment.MapPath("~/Images") + @"\I" + new Guid().ToString() + ".png", FileMode.Create);
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
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter as fotos.");
            }
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}