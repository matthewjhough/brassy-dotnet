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
            Name = "Subscription";
            Description = "Observable stream of items added to the database";
            AddField (new EventStreamFieldType {
                Name = "messageAdded",
                    Type = typeof (MessageType),
                    Resolver = new FuncFieldResolver<MessageModel> (
                        context => context.Source as MessageModel
                    ),
                    Subscriber = new EventStreamResolver<MessageModel> (SubscribeToMessages)
            });
        }

        private IObservable<MessageModel> SubscribeToMessages (ResolveEventStreamContext context) {
            return _messageRepository.Messages ();
        }
    }
}