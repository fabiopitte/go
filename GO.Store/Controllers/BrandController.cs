using System.Web.Mvc;

namespace GO.Controllers
{
    public class BrandController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Cadastro de Marcas";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Lista de Marcas";

            return View();
        }
    }
}