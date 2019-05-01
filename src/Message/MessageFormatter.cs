using System;
using System.Collections.Generic;
using System.Linq;
using brassy_api.src.Mood;

namespace brassy_api.src.Message {
    public class MessageFormatter {
        /// <summary>
        /// Takes IEnumerable of MessageModels, and formats content of messages to MoodModel specified format.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IEnumerable<MessageModel> FormatMessages (IEnumerable<MessageModel> messages) {
            return messages.Select (msg => AddMoodFormat (msg));
        }

        /// <summary>
        /// Takes message, and applies mood modifiers.
        /// </summary>
        /// <param name="message">MoodModel</param>
        /// <returns>MoodModel with content string formatted.</returns>
        public static MessageModel AddMoodFormat (MessageModel message) {
            if (message.Mood == MoodModel.ANGRY) {
                message.Content = message.Content.ToUpper ();
            } else if (message.Mood == MoodModel.NEUTRAL) {
                message.Content = message.Content.ToLower ();
            } else if (message.Mood == MoodModel.BORED) {
                message.Content = RandomlyCapitalize (message.Content);
            }
            return message;
        }

        /// <summary>
        /// Takes a string and randomly capitalizes its letters.
        /// </summary>
        /// <param name="msg">string</param>
        /// <returns>string with random letters capitalized.</returns>
        public static string RandomlyCapitalize (string msg) {
            string newString = "";
            for (var i = 0; i < msg.Length; i++) {
                var random = new Random ();
                newString += random.Next (100) < 40 ?
                    msg[i]
                    .ToString ()
                    .ToLower () :
                    msg[i]
                    .ToString ()
                    .ToUpper ();
            }

            return newString;
        }
    }
}