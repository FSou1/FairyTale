using FT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FT.MvcApp.Shared.HtmlHelpers
{
    public static class TagHtmlHelpers
    {
        public static MvcHtmlString ActionLink(this HtmlHelper self, Tag tag)
        {
            return self.ActionLink(tag.Title, "Single", "Tags", new { id = tag.Id }, new { @class = "tag" });
        }

        public static MvcHtmlString ActionLink(this HtmlHelper self, Tag tag, string title)
        {
            return self.ActionLink(title, "Single", "Tags", new { id = tag.Id }, new { @class = "tag" });
        }

        public static string DisplayName(this HtmlHelper self, TagType type) {
            switch (type) {
                case TagType.Author:
                    return "Авторские";
                case TagType.Folk:
                    return "Народные";
                case TagType.Other:
                    return "Другие";
                default:
                    return "Неизвестно";
            }
        }
    }
}