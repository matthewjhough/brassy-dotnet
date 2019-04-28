using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using brassy_api.src.Data;
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
            return messages;
        }

        public async Task<MessageModel> AddMessage (MessageModel message) {
            message.Id = Guid.NewGuid ().ToString ();
            var addedMessage = await _db.Messages.AddAsync (message);
            await _db.SaveChangesAsync ();
            return addedMessage.Entity;
        }
    }
}