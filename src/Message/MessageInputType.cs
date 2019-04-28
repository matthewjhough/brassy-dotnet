using brassy_api.src.Mood;
using GraphQL.Types;

namespace brassy_api.src.Message {
    public class MessageInputType : InputObjectGraphType {
        public MessageInputType () {
            Name = "MessageInput";
            Field<NonNullGraphType<StringGraphType>> ("content");
            Field<NonNullGraphType<MoodType>> ("mood");
        }
    }
}