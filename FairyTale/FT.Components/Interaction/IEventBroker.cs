using System.Threading.Tasks;

namespace FT.Components.Interaction {
    public interface IEventBroker {
        Task TaleViewed(int id);
    }
}