using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Facebook;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Fs.Container;
using Fs.Container.Lifetime;
using FT.Components.Interaction;
using FT.Components.Serializer;
using FT.Components.Serializer.Json;
using FT.Entities;
using FT.MvcApp.AlphaIndex.Services;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Home.Services;
using FT.MvcApp.Social.Services;
using FT.MvcApp.Tags.Services;
using FT.MvcApp.TagSuggestions.Services;
using FT.Repositories;
using FT.Repositories.Fake;
using FT.Repositories.NHibernate;
using log4net;
using LinqToTwitter;
using NHibernate;
using VkNet;

namespace FT.MvcApp
{
    public class IoCConfig
    {
        public static void RegisterComponents() {
            var container = new FsContainer();

            container.For<ISerializer>().Use<NewtonsoftJsonSerializer>(new ContainerControlledLifetimeManager());

            container.For<ITagsBuilder>().Use<TagsBuilder>(new PerHttpRequestLifetimeManager());
            container.For<IHomeBuilder>().Use<HomeBuilder>(new PerHttpRequestLifetimeManager());
            container.For<IFairyTalesBuilder>().Use<FairyTalesBuilder>(new PerHttpRequestLifetimeManager());
            container.For<IAlphaIndexBuilder>().Use<AlphaIndexBuilder>(new PerHttpRequestLifetimeManager());

            container.For<IFairyTalesService>().Use<FairyTalesService>(new PerHttpRequestLifetimeManager());
            container.For<ITagSuggestionService>().Use<TagSuggestionService>(new PerHttpRequestLifetimeManager());

            container.For<IEventBroker>().Use<HttpEventBroker>(new ContainerControlledLifetimeManager())
                .WithConstructorArgument("host", AppPropertyKeys.BaseUrl)
                .WithConstructorArgument("token", AppPropertyKeys.BaseAccessToken);

            #region Twitter

            container.For<IAuthorizer>().Use(ctx => new SingleUserAuthorizer {
                CredentialStore = new SingleUserInMemoryCredentialStore {
                    ConsumerKey = AppPropertyKeys.TwitterConsumerKey,
                    ConsumerSecret = AppPropertyKeys.TwitterConsumerSecret,
                    AccessToken = AppPropertyKeys.TwitterAccessToken,
                    AccessTokenSecret = AppPropertyKeys.TwitterAccessTokenSecret
                }
            });
            container.For<ITwitter>().Use<TwitterService>(new PerHttpRequestLifetimeManager());
            #endregion

            #region Facebook

            container.For<FacebookClient>().Use(ctx => new FacebookClient(AppPropertyKeys.FacebookAccessToken));
            container.For<IFacebook>().Use<FacebookService>(new PerHttpRequestLifetimeManager());
            #endregion

            #region Vk

            container.For<VkApi>().Use(ctx => { 
                var vk = new VkApi();
                vk.Authorize(new ApiAuthParams() {
                    ApplicationId = AppPropertyKeys.VkApplicationId,
                    Login = AppPropertyKeys.VkLogin,
                    Password = AppPropertyKeys.VkPassword,
                    Settings = VkNet.Enums.Filters.Settings.Wall
                });
                return vk;
            });
            container.For<IVkontakte>().Use(ctx => new VkService(ctx.Resolve<VkApi>(), AppPropertyKeys.VkPageId), 
                new PerHttpRequestLifetimeManager());
            #endregion

            RegisterDatabase(container);

            DependencyResolver.SetResolver(new FsContainerDependencyResolver(container));
        }

        public static void RegisterStub(IFsContainer container)
        {
            container.For<IRepository<FairyTale>>().Use<FakeFairyTaleRepository>(
                new PerHttpRequestLifetimeManager());
            container.For<IRepository<Tag>>().Use<FakeTagRepository>(
                new PerHttpRequestLifetimeManager());
        }

        public static void RegisterDatabase(IFsContainer container)
        {
            container.For<ISessionFactory>().Use(ctx =>
                Fluently
                    .Configure()
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<FairyTale>())
                    .Database(MsSqlConfiguration
                        .MsSql2012
                        .ConnectionString(x => x.FromConnectionStringWithKey("db")))
                    .BuildSessionFactory()
                , new ContainerControlledLifetimeManager());

            container.For<IUnitOfWork>().Use<NHibernateUnitOfWork>(new PerHttpRequestLifetimeManager());
            container.For<IRepository<FairyTale>>().Use<Repository<FairyTale>>(new PerHttpRequestLifetimeManager());
            container.For<IFairyTaleRepository>().Use<FairyTaleRepository>(new PerHttpRequestLifetimeManager());
            container.For<IRepository<Tag>>().Use<Repository<Tag>>(new PerHttpRequestLifetimeManager());
            container.For<ITagRepository>().Use<TagRepository>(new PerHttpRequestLifetimeManager());
            container.For<IRepository<SuggestedTag>>().Use<Repository<SuggestedTag>>(new PerHttpRequestLifetimeManager());
            container.For<IRepository<TagSuggestion>>().Use<Repository<TagSuggestion>>(new PerHttpRequestLifetimeManager());
        }
    }

    public class PerHttpRequestLifetimeManager : ILifetimeManager
    {
        private readonly Guid _key = Guid.NewGuid();

        public object GetValue()
            => HttpContext.Current.Items[_key];

        public void SetValue(object newValue)
            => HttpContext.Current.Items[_key] = newValue;

        public void RemoveValue()
        {
        }
    }

    public class FsContainerDependencyResolver : IDependencyResolver {
        private readonly IFsContainer _container;

        public FsContainerDependencyResolver(IFsContainer container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            try {
                return _container.Resolve(serviceType);
            }
            catch {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            try {
                return new[] {_container.Resolve(serviceType)};
            }
            catch {
                return new List<object>();
            }
        }
    }
}
