using System.Web.Mvc;
using Facebook;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FT.Components.Serializer;
using FT.Components.Serializer.Json;
using FT.Entities;
using FT.MvcApp.AlphaIndex.Services;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Home.Services;
using FT.MvcApp.Social.Services;
using FT.MvcApp.Tags.Services;
using FT.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using FT.Repositories.Fake;
using FT.Repositories.NHibernate;
using LinqToTwitter;
using VkNet;

namespace FT.MvcApp
{
    public class UnityConfig
    {
        public static void RegisterComponents() {
            var container = new UnityContainer();

            container.RegisterType<ISerializer, NewtonsoftJsonSerializer>();

            container.RegisterType<ITagsBuilder, TagsBuilder>(new PerRequestLifetimeManager());
            container.RegisterType<IHomeBuilder, HomeBuilder>(new PerRequestLifetimeManager());
            container.RegisterType<IFairyTalesBuilder, FairyTalesBuilder>(new PerRequestLifetimeManager());
            container.RegisterType<IAlphaIndexBuilder, AlphaIndexBuilder>(new PerRequestLifetimeManager());

            container.RegisterType<IFairyTalesService, FairyTalesService>(new PerRequestLifetimeManager());

            #region Twitter

            container.RegisterType<IAuthorizer>(new InjectionFactory(c => new SingleUserAuthorizer {
                CredentialStore = new SingleUserInMemoryCredentialStore {
                    ConsumerKey = AppPropertyKeys.TwitterConsumerKey,
                    ConsumerSecret = AppPropertyKeys.TwitterConsumerSecret,
                    AccessToken = AppPropertyKeys.TwitterAccessToken,
                    AccessTokenSecret = AppPropertyKeys.TwitterAccessTokenSecret
                }
            }));
            container.RegisterType<ITwitter, TwitterService>(new PerRequestLifetimeManager());
            #endregion

            #region Facebook

            container.RegisterType<FacebookClient>(new InjectionFactory(c =>
                new FacebookClient(AppPropertyKeys.FacebookAccessToken)
            ));
            container.RegisterType<IFacebook, FacebookService>(new PerRequestLifetimeManager());
            #endregion

            #region Vk

            container.RegisterType<VkApi>(new InjectionFactory(c => {
                var vk = new VkApi();
                vk.Authorize(new ApiAuthParams() {
                    ApplicationId = AppPropertyKeys.VkApplicationId,
                    Login = AppPropertyKeys.VkLogin,
                    Password = AppPropertyKeys.VkPassword,
                    Settings = VkNet.Enums.Filters.Settings.Wall
                });
                return vk;
            }));
            container.RegisterType<IVkontakte, VkService>(new PerRequestLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter(typeof(VkApi)), AppPropertyKeys.VkPageId)
            );
            #endregion

            //RegisterStub(container);
            RegisterDatabase(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterStub(IUnityContainer container)
        {
            container.RegisterType<IRepository<FairyTale>, FakeFairyTaleRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Tag>, FakeTagRepository>(new PerRequestLifetimeManager());
        }

        public static void RegisterDatabase(IUnityContainer container)
        {
            container.RegisterInstance(
                Fluently
                    .Configure()
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<FairyTale>())
                    .Database(MsSqlConfiguration
                        .MsSql2012
                        .ConnectionString(x => x.FromConnectionStringWithKey("db")))
                    .BuildSessionFactory()
                );

            container.RegisterType<IUnitOfWork, NHibernateUnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<FairyTale>, Repository<FairyTale>>(new PerRequestLifetimeManager());
            container.RegisterType<IFairyTaleRepository, FairyTaleRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Tag>, Repository<Tag>>(new PerRequestLifetimeManager());
            container.RegisterType<ITagRepository, TagRepository>(new PerRequestLifetimeManager());
        }
    }
}
