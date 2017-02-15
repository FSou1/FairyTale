using FT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FT.MvcApp.Shared.HtmlHelpers {
    public static class FairyTaleHtmlHelpers {
        public static MvcHtmlString ActionLink(this HtmlHelper self, FairyTale fairyTale) {
            return self.ActionLink(fairyTale, null);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper self, FairyTale fairyTale, object htmlAttributes)
        {
            return self.ActionLink(fairyTale.Title, "Single", "FairyTales", new { id = fairyTale.Id }, htmlAttributes);
        }
    }
}