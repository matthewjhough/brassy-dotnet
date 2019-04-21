using GraphQL.Types;

namespace brassy_api.src.Message {
    public class MessageType : ObjectGraphType<MessageModel> {
        public MessageType () {
            Field (x => x.Id).Description ("The Id of the Message");
            Field (x => x.Content, nullable : false).Description ("The user input Content of the Message.");
            Field (x => x.UserId, nullable : false).Description ("The User Id associated with message.");
            Field (x => x.SessionId, nullable : false).Description ("The Session Id associated with message.");
        }
    }
}