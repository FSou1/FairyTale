using System.Collections.Generic;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.AlphaIndex.Models
{
    public class IndexParams
    {
        
    }

    public class IndexViewModel : LayoutViewModel
    {
        public IList<char> TalesLetters { get; set; }
        public IList<char> TagsLetters { get; set; }
    }
}