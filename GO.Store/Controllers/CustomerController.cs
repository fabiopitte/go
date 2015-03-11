using System.Web.Mvc;

namespace GO.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Cadastro de Clientes";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Lista de Clientes";

            return View();
        }
    }
}