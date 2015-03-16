using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GO.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Cadastro de Produtos";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Produtos Disponíveis";

            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\products", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "photos");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                    }
                }
            }
            catch
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Erro ao salvar o arquivo" });
            }
        }
    }
}