using FT.Entities;
using FT.MvcApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.Tags.Models
{
    public class IndexViewModel : LayoutViewModel
    {
        public IList<Tag> Tags { get; set; }
    }
}