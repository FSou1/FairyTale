using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FT.Components.Extension;
using FT.Components.Serializer;
using FT.Components.Utility;
using FT.Entities;
using FT.MvcApp.FairyTales.Services;
using LinqToTwitter;

namespace FT.MvcApp.Social.Controllers {
    public class SocialController : Controller {
        public SocialController(
            IFairyTalesService talesService,
            ISerializer serializer,
            IAuthorizer twitterAuth
        ) {
            _talesService = talesService;
            _serializer = serializer;
            _twitterAuth = twitterAuth;
        }

        public async Task<ActionResult> Twitter() {
            await _twitterAuth.AuthorizeAsync();

            var ctx = new TwitterContext(_twitterAuth);

            var tale = await GetRandomTale();
            var message = GetMessage(tale, 140);

            var status = await ctx.TweetAsync(message);

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
        private readonly IAuthorizer _twitterAuth;
    }
}