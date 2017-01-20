using System.Collections.Generic;
using FT.Entities;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.Home.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public IList<FairyTale> RandomFairyTales { get; set; }
    }
}