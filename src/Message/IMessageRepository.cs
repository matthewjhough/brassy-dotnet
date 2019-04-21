using System.Collections.Generic;
using System.Threading.Tasks;

namespace brassy_api.src.Message {
    public interface IMessageRepository {
        Task<IEnumerable<MessageModel>> Get ();
    }
}