using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Tags.Models {
    public class SingleViewModel : LayoutViewModel {
        public Tag Tag { get; set; }
        public IPagedList<FairyTale> FairyTales { get; set; }
    }

    public class SingleParams
    {
        public int Id { get; set; }
        public int? Page { get; set; }

        public int CurrentPage => Page - 1 ?? 0;
    }
}