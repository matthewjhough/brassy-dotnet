using System.Collections.Generic;
using brassy_api.src.Message;
using GraphQL.Types;

namespace brassy_api.src.Operations {
    public class QueryResolver : ObjectGraphType {
        public QueryResolver (IMessageRepository _messageRepository) {
            Field<ListGraphType<MessageType>> (
                "messages",
                resolve : context => _messageRepository.Get ()
            );
        }
    }
}