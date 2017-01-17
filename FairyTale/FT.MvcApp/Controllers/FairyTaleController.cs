using FT.Entities;
using FT.MvcApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FT.MvcApp.Controllers
{
    public class FairyTaleController : Controller
    {
        // GET: FairyTale
        public ActionResult Index()
        {
            var tales = new List<FairyTaleVm>() {
                new FairyTaleVm {
                    Id = 1,
                    Title = "First",
                    CreatedAtUtc = DateTime.UtcNow
                }
            };

            return View("Index", tales);
        }
    }
}