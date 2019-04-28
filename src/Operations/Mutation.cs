using System;
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
                    long dateticks = DateTime.Now.Ticks;
                    long datemilliseconds = dateticks / TimeSpan.TicksPerMillisecond;
                    var message = MessageRepository.AddMoodFormat (context.GetArgument<MessageModel> ("message"));
                    message.CreatedAt = datemilliseconds;
                    _logger.LogInformation ($"Mood sent: {message.Mood.ToString()}");
                    return _messageRepository.AddMessage (message);
                });
        }
    }
}