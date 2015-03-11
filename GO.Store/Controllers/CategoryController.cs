using System.Web.Mvc;

namespace GO.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Cadastro de Categorias";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Lista de Categorias";

            return View();
        }
    }
}