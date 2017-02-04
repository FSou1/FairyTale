using System.Web.Mvc;
using FT.MvcApp.Filters;

namespace FT.MvcApp {
    public class FiltersConfig {
        public static void Configure() {
            GlobalFilters.Filters.Add(new ExceptionHandlerAttribute());
        }
    }
}