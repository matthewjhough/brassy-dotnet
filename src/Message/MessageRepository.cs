using System;
using System.Collections.Concurrent;
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

        private readonly ISubject<List<MessageModel>> _allMessageStream = new ReplaySubject<List<MessageModel>> (1);

        public ConcurrentStack<MessageModel> _messages { get; }

        public MessageRepository (BrassyContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
            _messages = new ConcurrentStack<MessageModel> ();
        }

        /// <summary>
        /// Returns observable message stream.
        /// </summary>
        /// <returns></returns>
        public IObservable<MessageModel> Messages () {
            _logger.LogInformation (_messageStream.ToString ());
            return _messageStream.AsObservable ();
        }

        /// <summary>
        /// Gets all messages in the correct format
        /// </summary>
        /// <returns>List of messages</returns>
        public async Task<IEnumerable<MessageModel>> AllMessages () {
            _logger.LogInformation ("Getting all messages...");
            var messages = await _db.Messages.ToListAsync ();
            var formattedMessages = MessageFormatter.FormatMessages (messages).OrderByDescending (msg => msg.CreatedAt);

            return formattedMessages;
        }

        /// <summary>
        /// Add message records to the database.
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public async Task<MessageModel> AddMessage (MessageModel message) {
            message.Id = Guid.NewGuid ().ToString ();
            var addedMessage = await _db.Messages.AddAsync (message);
            await _db.SaveChangesAsync ();
            // send message to Observable stream.
            _messages.Push (message);
            _messageStream.OnNext (message);

            return addedMessage.Entity;
        }

    }
}