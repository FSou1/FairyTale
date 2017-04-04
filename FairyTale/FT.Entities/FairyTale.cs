using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using FT.Entities.Contract;

namespace FT.Entities {
    public class FairyTale : IEntity {
        public virtual int Id { get; set; }        
        public virtual FairyTale Parent { get; set; }
        public virtual FairyTale Previous { get; set; }
        public virtual FairyTale Next { get; set; }
        public virtual string Title { get; set; }
        public virtual string Teaser { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual string Description { get; set; }
        public virtual char FirstLetter { get; set; }

        public virtual bool IsBook => Children.Count > 0;
        public virtual bool HasNavigation => Parent != null || Previous != null || Next != null;

        public virtual FairyTaleSummary Summary { get; set; } = new FairyTaleSummary();
        public virtual ISet<FairyTale> Children { get; set; } = new HashSet<FairyTale>();
        public virtual ISet<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
    
    public class FairyTaleSummary
    {
        public virtual int EstimatedReadingMinutes { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Views { get; set; }
    }

    public class FairyTaleMap : ClassMap<FairyTale> {
        public FairyTaleMap() {
            Schema("[ft]");
            Table("[FairyTales]");

            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.Parent)
                .Column("ParentId");

            References(x => x.Previous)
                .Column("PreviousId");

            References(x => x.Next)
                .Column("NextId");

            Map(x => x.Title);
            Map(x => x.Teaser);
            Map(x => x.Text).CustomType("StringClob").CustomSqlType("nvarchar(max)");
            Map(x => x.CreatedAtUtc);
            Map(x => x.Description);
            Map(x => x.FirstLetter);

            Component(x => x.Summary, part => {
                part.Map(p => p.EstimatedReadingMinutes, "EstimatedReadingMinutes");
                part.Map(p => p.Likes, "Likes");
                part.Map(p => p.Views, "Views");
            });

            HasMany(x => x.Children)
                .Schema("[ft]")
                .Table("[FairyTales]")
                .KeyColumn("ParentId")
                .ExtraLazyLoad()
                .Fetch.Join();

            HasManyToMany(x => x.Tags)
                .Schema("[ft]")
                .Table("[FairyTales_Tags]")
                .ParentKeyColumn("FairyTaleId")
                .ChildKeyColumn("TagId")
                .Fetch.Join();
        }
    }
}
