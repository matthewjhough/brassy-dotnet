using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace brassy_api.src.Message {
    public interface IMessageRepository {
        Task<IEnumerable<MessageModel>> AllMessages ();
        Task<MessageModel> AddMessage (MessageModel message);
        IObservable<MessageModel> MessageCreated ();
    }
}