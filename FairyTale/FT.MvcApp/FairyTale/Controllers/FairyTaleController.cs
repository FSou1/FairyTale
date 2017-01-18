using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FT.MvcApp.FairyTale.Models;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.FairyTale.Controllers
{
    public class FairyTaleController : Controller
    {
        // GET: FairyTale
        public ActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Title = "Главная страница",
                FairyTales = new List<FairyTaleViewModel>() {
                    new FairyTaleViewModel {
                        Id = 1,
                        Title = "First",
                        CreatedAtUtc = DateTime.UtcNow
                    }
                }
            };

            return View("Index", model);
        }
    }
}