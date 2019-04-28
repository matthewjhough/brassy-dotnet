using System.Collections.Generic;
using brassy_api.src.Message;
using brassy_api.src.Mood;
using GraphQL.Types;

namespace brassy_api.src.Operations {
    public class Query : ObjectGraphType {
        public Query (IMessageRepository _messageRepository) {
            Field<ListGraphType<MessageType>> (
                "messages",
                "A list of all messages sent.",
                resolve : context => _messageRepository.Get ()
            );
            Field<ListGraphType<MoodType>> (
                "moods",
                "A temporary state of mind or feeling that will alter the appearance of content."
            );
        }
    }
}