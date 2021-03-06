﻿using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FT.Components.Logger;

namespace FT.MvcApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.RegisterComponents();
            LoggerConfig.Configure(AppPropertyKeys.LogglyInputKey);
            FiltersConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

    public class AppPropertyKeys
    {
        public static int TalesPerPage = 5;

        public static string TwitterConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
        public static string TwitterConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
        public static string TwitterAccessToken = ConfigurationManager.AppSettings["TwitterAccessToken"];
        public static string TwitterAccessTokenSecret = ConfigurationManager.AppSettings["TwitterAccessTokenSecret"];
        public static string FacebookAccessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        public static ulong VkApplicationId = ulong.Parse(ConfigurationManager.AppSettings["VkApplicationId"] ?? "0");
        public static string VkLogin = ConfigurationManager.AppSettings["VkLogin"];
        public static string VkPassword = ConfigurationManager.AppSettings["VkPassword"];
        public static long VkPageId = long.Parse(ConfigurationManager.AppSettings["VkPageId"] ?? "0");
        public static string OkAccessToken = ConfigurationManager.AppSettings["OkAccessToken"];
        public static string LogglyInputKey = ConfigurationManager.AppSettings["LogglyInputKey"];
        public static string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public static string BaseAccessToken = ConfigurationManager.AppSettings["BaseAccessToken"];
    }
}
