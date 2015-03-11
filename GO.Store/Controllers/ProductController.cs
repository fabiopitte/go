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
            ViewBag.Titulo = "Lista de Produtos";

            return View();
        }
    }
}