using System;
using System.Threading.Tasks;
using LinqToTwitter;

namespace FT.MvcApp.Social.Services {
    public class TwitterService : ITwitter {
        public TwitterService(IAuthorizer authorizer) {
            _authorizer = authorizer;
        }

        public async Task<TweetResult> TweetAsync(string message) {
            await _authorizer.AuthorizeAsync();

            var ctx = new TwitterContext(_authorizer);

            var result = await ctx.TweetAsync(message);

            return new TweetResult {
                Text = result.Text,
                CreatedAt = result.CreatedAt
            };
        }

        private readonly IAuthorizer _authorizer;
    }

    public class TweetResult {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public interface ITwitter {
        Task<TweetResult> TweetAsync(string message);
    }
}