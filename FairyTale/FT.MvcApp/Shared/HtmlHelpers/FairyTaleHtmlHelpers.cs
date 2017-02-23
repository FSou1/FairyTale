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

        public static MvcHtmlString ActionLink(this HtmlHelper self, FairyTale fairyTale, object htmlAttributes) {
            if (fairyTale == null) return EmptyString;
            return self.ActionLink(fairyTale.Title, fairyTale, htmlAttributes);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper self, string title, FairyTale fairyTale, object htmlAttributes) {
            if (fairyTale == null) return EmptyString;
            return self.ActionLink(title, "Single", "FairyTales", new {id = fairyTale.Id}, htmlAttributes);
        }

        public static MvcHtmlString Previous(this HtmlHelper self, FairyTale fairyTale) {
            if (fairyTale == null) return EmptyString;
            return self.ActionLink("Назад к " + fairyTale.Title, fairyTale, new { @class = "prev-arr" });
        }

        public static MvcHtmlString Parent(this HtmlHelper self, FairyTale fairyTale) {
            if (fairyTale == null) return EmptyString;
            return self.ActionLink("К оглавлению", fairyTale, null);
        }

        public static MvcHtmlString Next(this HtmlHelper self, FairyTale fairyTale) {
            if (fairyTale == null) return EmptyString;
            return self.ActionLink("Вперёд к " + fairyTale.Title, fairyTale, new { @class = "next-arr" });
        }

        private static MvcHtmlString EmptyString => new MvcHtmlString("&nbsp;");
    }
}