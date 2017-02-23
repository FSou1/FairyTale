using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FT.Entities.Contract;

namespace FT.Entities {
    public class Tag : IEntity {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int FairyTalesCount { get; set; }
               
        public virtual IList<FairyTale> FairyTales { get; set; } = new List<FairyTale>();
    }

    public class TagMap : ClassMap<Tag> {
        public TagMap() {
            Schema("[ft]");
            Table("[Tags]");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.FairyTalesCount);

            HasManyToMany(x => x.FairyTales)
                .Schema("[ft]")
                .Table("[FairyTales_Tags]")
                .ParentKeyColumn("TagId")
                .ChildKeyColumn("FairyTaleId");
        }
    }
}