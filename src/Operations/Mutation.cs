using brassy_api.src.Message;
using GraphQL.Types;

namespace brassy_api.src.Operations {
    public class Mutation : ObjectGraphType {
        public Mutation (IMessageRepository _messageRepository) {
            Field<MessageType> (
                "createMessage",
                arguments : new QueryArguments (
                    new QueryArgument<NonNullGraphType<MessageInputType>> { Name = "message" }
                ),
                resolve : context => {
                    var message = context.GetArgument<MessageModel> ("message");
                    return _messageRepository.AddMessage (message);
                });
        }
    }
}