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
    }

    public class SearchParams
    {
        public string Term { get; set; }
    }
}