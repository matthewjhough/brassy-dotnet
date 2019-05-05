using brassy_api.src.Controllers;
using brassy_api.src.Operations;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public class MessageSchema : Schema {
        public MessageSchema (
            IMessageRepository messageRepository
        ) {
            Query = new Query (messageRepository);
            Mutation = new Mutation (messageRepository);
            Subscription = new Subscription (messageRepository);
        }
    }
}