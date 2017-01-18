using System.Web.Mvc;

namespace FT.MvcApp.Home.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}