using FT.Entities;
using FT.MvcApp.Shared.Models;
using MvcPaging;

namespace FT.MvcApp.Tags.Models {
    public class SingleViewModel : BaseViewModel {
        public Tag Tag { get; set; }
        public IPagedList<FairyTale> FairyTales { get; set; }
    }
}