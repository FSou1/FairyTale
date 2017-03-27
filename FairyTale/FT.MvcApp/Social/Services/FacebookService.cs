using System.Collections.Generic;
using System.Threading.Tasks;
using Facebook;

namespace FT.MvcApp.Social.Services {
    public class FacebookService : IFacebook {
        public FacebookService(FacebookClient client) {
            _client = client;
        }

        public Task<PostFbMessageResult> PostAsync(string message) {
            var result = _client.Post("feed", new {message});
            var json = (JsonObject) result;
            
            return Task.FromResult(new PostFbMessageResult() {
                Id = $"{json["id"]}"
            });
        }

        private readonly FacebookClient _client;
    }

    public class PostFbMessageResult {
        public string Id;
    }

    public interface IFacebook {
        Task<PostFbMessageResult> PostAsync(string message);
    }
}