using System;
using brassy_api.src.Message;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Operations {
    public class Mutation : ObjectGraphType {
        public Mutation (IMessageRepository _messageRepository) {
            Field<MessageType> (
                "createMessage",
                arguments : new QueryArguments (
                    new QueryArgument<NonNullGraphType<MessageInputType>> { Name = "message" }
                ),
                resolve : context => {
                    long dateticks = DateTime.Now.Ticks;
                    long datemilliseconds = dateticks / TimeSpan.TicksPerMillisecond;
                    var message = MessageFormatter.AddMoodFormat (context.GetArgument<MessageModel> ("message"));
                    message.CreatedAt = datemilliseconds;
                    message.Id = Guid.NewGuid ().ToString ();

                    var addedMessage = _messageRepository.AddMessage (message);
                    return addedMessage;
                });
        }
    }
}