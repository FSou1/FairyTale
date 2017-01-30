using System.Linq;
using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Home.Models
{
    public class SearchViewModel : LayoutViewModel
    {
        public string Term { get; set; }
        public bool Found => FoundFairyTales.Any();
        public IPagedList<FairyTale> FoundFairyTales { get; set; }
    }

    public class SearchParams
    {
        public string Term { get; set; }
        public int? Page { get; set; }

        public int CurrentPage => Page - 1 ?? 0;
    }
}