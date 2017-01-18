using System.Collections.Generic;
using FT.MvcApp.Shared.Models;
using FT.MvcApp.ViewModels;

namespace FT.MvcApp.FairyTale.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public IList<FairyTaleViewModel> FairyTales { get; set; }
    }
}