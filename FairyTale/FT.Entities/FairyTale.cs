using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace FT.Entities {
    public class FairyTale {
        public virtual int Id { get; set; }        
        public virtual string Title { get; set; }
        public virtual string Teaser { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual string Description { get; set; }
        
        public virtual IList<Tag> Tags { get; set; } = new List<Tag>();
    }

    public class FairyTaleMap : ClassMap<FairyTale> {
        public FairyTaleMap() {
            Schema("[ft]");
            Table("[FairyTales]");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title);
            Map(x => x.Teaser);
            Map(x => x.Text).CustomType("StringClob").CustomSqlType("nvarchar(max)");
            Map(x => x.CreatedAtUtc);
            Map(x => x.Description);
            
            HasManyToMany(x => x.Tags)
                .Schema("[ft]")
                .Table("[FairyTales_Tags]")
                .ParentKeyColumn("FairyTaleId")
                .ChildKeyColumn("TagId");
        }
    }
}
