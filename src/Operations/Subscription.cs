using System;
using brassy_api.src.Message;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;

namespace brassy_api.src.Operations {
    public class Subscription : ObjectGraphType {
        private readonly IMessageRepository _messageRepository;
        public Subscription (IMessageRepository messageRepository) {
            _messageRepository = messageRepository;
            AddField (new EventStreamFieldType {
                Name = "messageAdded",
                    Type = typeof (MessageType),
                    Resolver = new FuncFieldResolver<MessageModel> (ResolveMessage),
                    Subscriber = new EventStreamResolver<MessageModel> (Subscribe)
            });
        }

        private MessageModel ResolveMessage (ResolveFieldContext context) {
            var message = context.Source as MessageModel;

            return message;
        }

        private IObservable<MessageModel> Subscribe (ResolveEventStreamContext context) {
            var messages = _messageRepository.Messages ();
            return messages;
        }
    }
}