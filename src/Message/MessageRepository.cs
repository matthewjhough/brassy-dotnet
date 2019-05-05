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
using Newtonsoft.Json;

namespace brassy_api.src.Message {
    public class MessageRepository : IMessageRepository {

        private BrassyContext _db { get; set; }

        private readonly ILogger _logger;

        private readonly ISubject<MessageModel> _messageStream = new ReplaySubject<MessageModel> (1);

        /// <summary>
        /// Message Repository constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public MessageRepository (BrassyContext db, ILogger<MessageRepository> logger) {
            _db = db;
            _logger = logger;
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
            var addedMessage = await _db.Messages.AddAsync (message);
            await _db.SaveChangesAsync ();

            // Send message to Observable stream.
            _logger.LogInformation (
                "*** Invoking _messageObserver.OnNext() *** : {0}",
                JsonConvert.SerializeObject (message)
            );
            _messageStream.OnNext (message);

            // Check observable status
            _logger.LogInformation (
                "*** Current _messsageObserver status *** : {0}",
                JsonConvert.SerializeObject (_messageStream)
            );

            return message;
        }

        /// <summary>
        /// Returns observable message stream.
        /// </summary>
        /// <returns></returns>
        public IObservable<MessageModel> MessageCreated () {
            // Check observable status
            _logger.LogInformation (
                "*** Current _messageStream status *** : {0}",
                JsonConvert.SerializeObject (_messageStream)
            );
            _logger.LogInformation ("*** Creating Watchable Observable... ***");

            return _messageStream.AsObservable ();
        }

    }
}