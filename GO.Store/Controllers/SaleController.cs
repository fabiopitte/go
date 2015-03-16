using System.Web.Mvc;

namespace GO.Controllers
{
    public class SaleController : Controller
    {
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;

            ViewBag.Titulo = "Saida de produto";

            return View();
        }
    }
}