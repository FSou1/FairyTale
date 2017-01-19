using System.Net;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.FairyTales.Services;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        private readonly FairyTalesService _service = new FairyTalesService();

        // GET: FairyTales
        public ActionResult Index()
        {
            var model = _service.BuildIndexViewModel();
            return View("Index", model);
        }

        // GET: Tale
        public ActionResult Single(int id)
        {
            var model = _service.BuildSingleViewModel(id);
            if (model.FairyTale == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }
    }
}