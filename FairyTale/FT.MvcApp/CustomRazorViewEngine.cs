using System.Web.Mvc;

namespace FT.MvcApp
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
        {                        
            ViewLocationFormats = new[]
            {
                "~/{1}/Views/{0}.cshtml",
            };

            PartialViewLocationFormats = new[]
            {
                "~/Shared/Views/Partial/{0}.cshtml"
            };
        }
    }
}