using brassy_api.src.Message;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Operations {
    public class Mutation : ObjectGraphType {
        ILogger _logger;
        public Mutation (IMessageRepository _messageRepository, ILogger<Mutation> logger) {
            _logger = logger;
            Field<MessageType> (
                "createMessage",
                arguments : new QueryArguments (
                    new QueryArgument<NonNullGraphType<MessageInputType>> { Name = "message" }
                ),
                resolve : context => {
                    var message = context.GetArgument<MessageModel> ("message");
                    _logger.LogInformation ($"Mood sent: {message.Mood.ToString()}");
                    return _messageRepository.AddMessage (message);
                });
        }
    }
}