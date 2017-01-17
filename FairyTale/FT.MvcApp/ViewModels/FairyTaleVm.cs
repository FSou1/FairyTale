using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.ViewModels {
    public class FairyTaleVm {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}