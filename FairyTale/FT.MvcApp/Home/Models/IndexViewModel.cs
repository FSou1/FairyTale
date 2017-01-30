using System.Collections.Generic;
using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Home.Models
{
    public class IndexViewModel : LayoutViewModel
    {
        public IPagedList<FairyTale> RandomFairyTales { get; set; }
    }

    public class IndexParams {
        public  int? Page { get; set; }

        public int CurrentPage => Page - 1 ?? 0;
    }
}