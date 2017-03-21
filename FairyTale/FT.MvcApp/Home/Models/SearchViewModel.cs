using System.Collections.Generic;
using System.Linq;
using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Home.Models
{
    public class SearchViewModel : LayoutViewModel
    {
        public string Query { get; set; }
        public IList<Tag> FoundTags { get; set; }
        public IPagedList<FairyTale> FoundFairyTales { get; set; }
        public IDictionary<string, object> RouteValues { get; set; }

        public bool TagsFound => FoundTags.Any();
        public bool TalesFound => FoundFairyTales.Any();
    }

    public class SearchParams
    {
        public string StartsWith { get; set; }
        public string Term { get; set; }
        public int? Page { get; set; }

        public int CurrentPage => Page - 1 ?? 0;

        public bool IsValid => !string.IsNullOrEmpty(Term) || !string.IsNullOrEmpty(StartsWith);
    }
}