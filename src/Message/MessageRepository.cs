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

        public ConcurrentStack<MessageModel> _allMessages { get; }

        private readonly ISubject<MessageModel> _messageObserver = new ReplaySubject<MessageModel> (100);

        /// <summary>
        /// Message Repository constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public MessageRepository (BrassyContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
            _allMessages = new ConcurrentStack<MessageModel> ();
        }

        /// <summary>
        /// Gets all messages in the correct format
        /// </summary>
        /// <returns>List of messages</returns>
        public async Task<IEnumerable<MessageModel>> AllMessages () {
            _logger.LogInformation ("Getting all messages...");
            var messages = await _db.Messages.ToListAsync ();
            var formattedMessages = MessageFormatter
                .FormatMessages (messages)
                .OrderByDescending (msg => msg.CreatedAt);

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
            _allMessages.Push (message);
            _messageObserver.OnNext (message);

            return addedMessage.Entity;
        }

        /// <summary>
        /// Returns observable message stream.
        /// </summary>
        /// <returns></returns>
        public IObservable<MessageModel> Messages () {
            _logger.LogInformation (_messageObserver.ToString ());
            return _messageObserver.AsObservable ();
        }

    }
}