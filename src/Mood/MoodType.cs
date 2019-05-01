using GraphQL.Types;

namespace brassy_api.src.Mood {
    public class MoodType : EnumerationGraphType<MoodModel> {
        public MoodType () {
            Name = "Mood";
            Description = "A temporary state of mind or feeling that will alter the appearance of content.";
            AddValue ("ANGRY", "Will appear in all caps.", 1);
            AddValue ("BORED", "Will randomly capitalize content letters.", 2);
            AddValue ("NEUTRAL", "Will appear all lower case.", 3);
            AddValue ("ENGAGED", "Content format exactly as written.", 4);
        }
    }
}