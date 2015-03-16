using System.Web.Mvc;

namespace GO.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}