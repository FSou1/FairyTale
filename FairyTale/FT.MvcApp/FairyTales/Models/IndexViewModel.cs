using System.Collections.Generic;
using FT.Entities;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.FairyTales.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public IList<FairyTale> FairyTales { get; set; }
    }
}