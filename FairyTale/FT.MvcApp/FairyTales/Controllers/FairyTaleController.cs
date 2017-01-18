using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FT.MvcApp.FairyTales.Models;
using FT.Entities;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        // GET: FairyTale
        public ActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Title = "Главная страница",
                FairyTales = new List<FairyTale>() {
                    new FairyTale {
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