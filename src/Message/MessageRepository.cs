using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using brassy_api.src.Data;
using brassy_api.src.Mood;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public class MessageRepository : IMessageRepository {
        private BrassyContext _db { get; set; }
        private readonly ILogger _logger;
        public MessageRepository (BrassyContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
        }
        public async Task<IEnumerable<MessageModel>> Get () {
            _logger.LogInformation ("Getting all messages...");
            var messages = await _db.Messages.ToListAsync ();
            var formattedMessages = MessageRepository.FormatMessages (messages);

            return formattedMessages;
        }

        public async Task<MessageModel> AddMessage (MessageModel message) {
            message.Id = Guid.NewGuid ().ToString ();
            var addedMessage = await _db.Messages.AddAsync (message);
            await _db.SaveChangesAsync ();
            return addedMessage.Entity;
        }

        /// <summary>
        /// Takes IEnumerable of MessageModels, and formats content of messages to MoodModel specified format.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IEnumerable<MessageModel> FormatMessages (IEnumerable<MessageModel> messages) {
            return messages.Select (msg => {
                if (msg.Mood == MoodModel.ANGRY) {
                    msg.Content = msg.Content.ToUpper ();
                } else if (msg.Mood == MoodModel.NEUTRAL) {
                    msg.Content = msg.Content.ToLower ();
                } else if (msg.Mood == MoodModel.BORED) {
                    msg.Content = MessageRepository.RandomlyCapitalize (msg.Content);
                }

                return msg;
            });
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