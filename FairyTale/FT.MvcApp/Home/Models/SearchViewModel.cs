using FT.Entities;
using FT.MvcApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.Home.Models
{
    public class SearchViewModel : BaseViewModel
    {
        public string Term { get; set; }

        public IList<FairyTale> FoundFairyTales { get; set; }
        public int TotalCount { get; set; }
        public bool IsFound => FoundFairyTales.Count > 0;

        public int CurrentPage { get; set; }        
        public int TotalPages => TotalCount / PerPage;
        private int PerPage { get; set; }

        public SearchViewModel(int perPage)
        {
            PerPage = perPage;
        }
    }

    public class SearchParams
    {
        public string Term { get; set; }
        public int Page { get; set; }
    }
}