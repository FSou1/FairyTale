using System.Web.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FT.Entities;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Home.Services;
using FT.MvcApp.Tags.Services;
using FT.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace FT.MvcApp
{
    public class UnityConfig
    {
        public static void RegisterComponents() {
            var container = new UnityContainer();

            container.RegisterInstance(
                Fluently
                    .Configure()
                    .Mappings(x=>x.FluentMappings.AddFromAssemblyOf<FairyTale>())
                    .Database(MsSqlConfiguration
                        .MsSql2012
                        .ConnectionString(x => x.FromConnectionStringWithKey("db")))
                    .BuildSessionFactory()
                );

            container.RegisterType<IFairyTalesService, FairyTalesService>(new PerRequestLifetimeManager());
            container.RegisterType<ITagsService, TagsService>(new PerRequestLifetimeManager());
            container.RegisterType<IHomeService, HomeService>(new PerRequestLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<FairyTale>, Repository<FairyTale>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Tag>, Repository<Tag>>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
