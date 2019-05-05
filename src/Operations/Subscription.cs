using System;
using brassy_api.src.Controllers;
using brassy_api.src.Message;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Operations {
    public class Subscription : ObjectGraphType {
        // private readonly IMessageRepository _messageRepository;
        public Subscription (IMessageRepository messageRepository) {
            Name = "Subscription";
            Description = "Observable stream of items added to the database";

            AddField (new EventStreamFieldType {
                Name = "messageAdded",
                    Type = typeof (MessageType),
                    Resolver = new FuncFieldResolver<MessageModel> (ResolveMessage),
                    Subscriber = new EventStreamResolver<MessageModel> (context => {
                        return messageRepository.MessageCreated ();
                    })
            });
        }

        private MessageModel ResolveMessage (ResolveFieldContext context) {
            var message = context.Source as MessageModel;

            return message;
        }

    }
}