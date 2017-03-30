using System.Collections.Generic;
using FT.Entities;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.FairyTales.Models
{
    public class SingleViewModel : LayoutViewModel
    {
        public FairyTale FairyTale { get; set; }
        public IList<FairyTale> Related { get; set; }
    }
}