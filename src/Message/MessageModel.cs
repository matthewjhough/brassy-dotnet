using brassy_api.src.Mood;

namespace brassy_api.src.Message {
    public class MessageModel {
        public string Id { get; set; }
        public string Content { get; set; }
        public MoodModel Mood { get; set; }
        public long CreatedAt { get; set; }
    }
}