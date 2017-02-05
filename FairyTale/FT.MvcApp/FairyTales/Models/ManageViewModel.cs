using FT.Entities;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.FairyTales.Models
{
    public class ManageViewModel : LayoutViewModel
    {
        public FairyTale FairyTale { get; set; }
        public FairyTale Next { get; set; }
        public int NextCount { get; set; }
    }
}