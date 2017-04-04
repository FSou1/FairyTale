using System.Threading.Tasks;
using RestSharp;

namespace FT.Components.Interaction {
    public class HttpEventBroker : IEventBroker {
        public HttpEventBroker(string host, string token) {
            _host = host;
            _token = token;
        }

        public Task TaleViewed(int id) {
            const string url = "/story/increase-views";

            var client = new RestClient(_host);
            var request = new RestRequest(url);
            request.AddParameter("id", id);
            request.AddParameter("token", _token);

            client.Execute(request);

            return Task.CompletedTask;
        }

        private readonly string _host;
        private readonly string _token;
    }
}