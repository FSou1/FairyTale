using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FT.Entities;

namespace FT.MvcApp.Shared.Models {
    public class TagsByTypeViewModel {
        public TagType Type { get; set; }
        public IDictionary<TagType, List<Tag>> Tags { get; set; }
    }
}