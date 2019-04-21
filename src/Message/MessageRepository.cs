using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public class MessageRepository : IMessageRepository {
        private MessageContext _db { get; set; }
        private readonly ILogger _logger;
        public MessageRepository (MessageContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
        }
        public async Task<IEnumerable<MessageModel>> Get () {
            _logger.LogInformation ("Getting all messages...");
            var messages = await _db.Messages.ToListAsync ();
            return messages;
        }
    }
}