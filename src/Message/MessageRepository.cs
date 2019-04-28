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
            var modifiedMessages = messages.Select (msg => {
                if (msg.Mood == MoodModel.ANGRY) {
                    msg.Content = msg.Content.ToUpper ();
                } else if (msg.Mood == MoodModel.NEUTRAL) {
                    msg.Content = msg.Content.ToLower ();
                } else if (msg.Mood == MoodModel.BORED) {
                    string newString = "";
                    for (var i = 0; i < msg.Content.Length; i++) {
                        var random = new Random ();
                        newString += random.Next (100) < 40 ?
                            msg.Content[i]
                            .ToString ()
                            .ToLower () :
                            msg.Content[i]
                            .ToString ()
                            .ToUpper ();
                    }
                    msg.Content = newString;
                }

                return msg;
            });

            return modifiedMessages;
        }

        public async Task<MessageModel> AddMessage (MessageModel message) {
            message.Id = Guid.NewGuid ().ToString ();
            var addedMessage = await _db.Messages.AddAsync (message);
            await _db.SaveChangesAsync ();
            return addedMessage.Entity;
        }
    }
}