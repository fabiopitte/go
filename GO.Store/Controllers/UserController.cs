using System.Web.Mvc;

namespace GO.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Configurações";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Configurações";

            return View();
        }
    }
}