using System.Collections.Generic;
using System.Threading.Tasks;
using Facebook;

namespace FT.MvcApp.Social.Services {
    public class FacebookService : IFacebook {
        public FacebookService(FacebookClient client) {
            _client = client;
        }

        public Task<PostMessageResult> PostAsync(string message) {
            var result = _client.Post("feed", new {message});
            var json = (JsonObject) result;
            
            return Task.FromResult(new PostMessageResult() {
                Id = $"{json["id"]}"
            });
        }

        private readonly FacebookClient _client;
    }

    public class PostMessageResult {
        public string Id;
    }

    public interface IFacebook {
        Task<PostMessageResult> PostAsync(string message);
    }
}