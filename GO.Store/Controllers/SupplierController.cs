using System.Web.Mvc;

namespace GO.Controllers
{
    public class SupplierController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Cadastro de Fornecedores";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Titulo = "Lista de Fornecedores";

            return View();
        }
    }
}