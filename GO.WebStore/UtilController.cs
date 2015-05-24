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

namespace GO.WebStore
{
    public class UtilController : ApiController
    {
        //[Authorize]
        [Route("product/photo/{url}")]
        [HttpGet]
        public HttpResponseMessage Get(string url)
        {
            try
            {
                var UrlBase = "C:/Projetos/go/OAuthServer.Api/Images/" + url;
                var UrlBaseVirtual = "http://localhost:60629/Images/" + url;

                var bmp = Bitmap.FromFile(UrlBase);

                var enderecoFisico = HostingEnvironment.MapPath("~/Images") + @"\" + url + ".png";
                var enderecoVirtual = "Images/" + url + ".png";

                if (!File.Exists(enderecoFisico))
                {
                    var Fs = new FileStream(enderecoFisico, FileMode.Create);
                    bmp.Save(Fs, ImageFormat.Png);

                    Fs.Dispose();
                }

                bmp.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, enderecoVirtual);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter as fotos.");
            }
        }
    }
}