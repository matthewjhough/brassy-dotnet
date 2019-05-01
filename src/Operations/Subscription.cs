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
            return context.Source as MessageModel;
        }

        private IObservable<MessageModel> Subscribe (ResolveEventStreamContext context) {
            return _messageRepository.Messages ();
        }
    }
}