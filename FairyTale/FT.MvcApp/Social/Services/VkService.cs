using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace FT.MvcApp.Social.Services {
    public class VkService : IVkontakte {
        public VkService(VkApi vkApi, long peerId) {
            _vkApi = vkApi;
            _peerId = peerId;
        }

        public Task<PostVkMessageResult> PostAsync(string message) {
            var messageId = _vkApi.Wall.Post(new WallPostParams {
                OwnerId = _peerId,
                Message = message
            });

            var result = new PostVkMessageResult {
                Id = messageId
            };

            return Task.FromResult(result);
        }

        private readonly VkApi _vkApi;
        private readonly long _peerId;
    }

    public class PostVkMessageResult {
        public long Id;
    }

    public interface IVkontakte {
        Task<PostVkMessageResult> PostAsync(string message);
    }
}