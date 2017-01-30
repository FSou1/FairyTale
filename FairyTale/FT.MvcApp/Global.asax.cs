﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FT.MvcApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
