using GraphQL.Types;

namespace brassy_api.src.Message {
    public class MessageType : ObjectGraphType<MessageModel> {
        public MessageType () {
            Field (x => x.Id).Description ("The Id of the Message");
            Field (x => x.Content, nullable : false).Description ("The user input Content of the Message.");
            Field (x => x.Mood, nullable : false).Description ("the current state of mind or feeling that will alter the appearance of content.");
        }
    }
}