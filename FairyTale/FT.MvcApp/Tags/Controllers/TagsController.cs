using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FT.MvcApp.Tags.Models;

namespace FT.MvcApp.Tags.Controllers {
    public class TagsController : Controller {
        // GET: Tag
        public ActionResult Single(int id) {
            var model = new SingleViewModel();
            return View("Single", model);
        }
    }
}