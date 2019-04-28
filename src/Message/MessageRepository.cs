using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using brassy_api.src.Data;
using brassy_api.src.Mood;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public class MessageRepository : IMessageRepository {
        private BrassyContext _db { get; set; }
        private readonly ILogger _logger;
        private readonly ISubject<MessageModel> _messageStream = new ReplaySubject<MessageModel> (1);
        public MessageRepository (BrassyContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
        }
        public async Task<IEnumerable<MessageModel>> AllMessages () {
            _logger.LogInformation ("Getting all messages...");
            var messages = await _db.Messages.ToListAsync ();
            var formattedMessages = FormatMessages (messages).OrderByDescending (msg => msg.CreatedAt);

            return formattedMessages;
        }

        public async Task<MessageModel> AddMessage (MessageModel message) {
            message.Id = Guid.NewGuid ().ToString ();
            var addedMessage = await _db.Messages.AddAsync (message);
            // send message to Observable stream.
            _messageStream.OnNext (message);
            await _db.SaveChangesAsync ();
            return addedMessage.Entity;
        }

        /// <summary>
        /// Returns observable message stream.
        /// </summary>
        /// <returns></returns>
        public IObservable<MessageModel> Messages () {
            return _messageStream.AsObservable ();
        }

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