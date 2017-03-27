using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using FT.Components.Extension;
using FT.Components.Serializer;
using FT.Components.Utility;
using FT.Entities;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Social.Services;

namespace FT.MvcApp.Social.Controllers {
    public class SocialController : Controller {
        public SocialController(
            IFairyTalesService talesService,
            ISerializer serializer,
            ITwitter twitter,
            IFacebook facebook,
            IVkontakte vkontakte
        ) {
            _talesService = talesService;
            _serializer = serializer;
            _twitter = twitter;
            _facebook = facebook;
            _vkontakte = vkontakte;
        }

        public enum PostType { RandomTale }

        /// <summary>
        /// Publish post
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ActionResult> Post(PostType type) {
            switch (type) {
                case PostType.RandomTale: {
                    var tale = await GetRandomTale();
                        
                    var fb = await Facebook(tale);
                    var tw = await Twitter(tale);
                    var vk = await Vkontakte(tale);

                    var result = new { fb, tw, vk };

                    return Content(_serializer.Serialize(result));
                }
                default: {
                    throw new InvalidEnumArgumentException(nameof(type));
                }
            }
        }

        /// <summary>
        /// Publish tale to vk page
        /// </summary>
        /// <param name="tale"></param>
        /// <returns></returns>
        private async Task<ActionResult> Vkontakte(FairyTale tale) {
            var message = GetMessage(tale);

            var post = await _vkontakte.PostAsync(message);

            var result = new {
                post.Id
            };

            return Content(_serializer.Serialize(result));
        }

        /// <summary>
        /// Publish tale to facebook page
        /// </summary>
        /// <returns></returns>
        private async Task<ActionResult> Facebook(FairyTale tale) {
            var message = GetMessage(tale);

            var post = await _facebook.PostAsync(message);

            var result = new {
                post.Id
            };

            return Content(_serializer.Serialize(result));
        }

        /// <summary>
        /// Publish tale to twitter account
        /// </summary>
        /// <returns></returns>
        private async Task<ActionResult> Twitter(FairyTale tale) {
            var message = GetMessage(tale, 140);

            var status = await _twitter.TweetAsync(message);

            var result = new {
                status.Text,
                status.CreatedAt
            };

            return Content(_serializer.Serialize(result));
        }

        private async Task<FairyTale> GetRandomTale() {
            var tale = await _talesService.GetRandomAsync();
            Guard.ArgumentNotNull(tale, nameof(tale));

            return tale;
        }

        private string GetMessage(FairyTale tale) {
            var url = " " + GetUrl(tale);

            var title = tale.Title;

            var message = $"{title}{url}";

            return message;
        }

        private string GetMessage(FairyTale tale, int maxLength) {
            var url = " " + GetUrl(tale);

            var title = tale.Title.Truncate(maxLength - url.Length);

            var message = $"{title}{url}".Truncate(maxLength);

            return message;
        }

        private string GetUrl(FairyTale tale) {
            return Url.Action("Single", "FairyTales", new {id = tale.Id}, "http");
        }

        private readonly IFairyTalesService _talesService;
        private readonly ISerializer _serializer;
        private readonly ITwitter _twitter;
        private readonly IFacebook _facebook;
        private readonly IVkontakte _vkontakte;
    }
}