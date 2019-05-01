using brassy_api.src.Operations;
using GraphQL.Types;

namespace brassy_api.src.Message {
    public class MessageSchema : Schema {
        public MessageSchema (IMessageRepository messageRepository) {
            Query = new Query (messageRepository);
            Mutation = new Mutation (messageRepository);
            Subscription = new Subscription (messageRepository);
        }
    }
}