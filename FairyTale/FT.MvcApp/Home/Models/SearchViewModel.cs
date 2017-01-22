using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Home.Models
{
    public class SearchViewModel : BaseViewModel
    {
        public string Term { get; set; }
                
        public IPagedList<FairyTale> FoundFairyTales { get; set; }
    }

    public class SearchParams
    {
        public string Term { get; set; }
        public int? Page { get; set; }

        public int CurrentPage => Page - 1 ?? 0;
    }
}