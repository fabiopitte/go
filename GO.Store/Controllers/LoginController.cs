using GO.Domain;
using System.Web.Mvc;

namespace GO.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User User)
        {
            if (ModelState.IsValid)
            {
            }
            //return new FilePathResult("~/Views/Error.html", "text/html");
            return RedirectToAction("Index", "DashBoard");
        }
    }
}