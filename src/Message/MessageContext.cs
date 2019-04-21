using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public class MessageContext : DbContext {
        public readonly ILogger _logger;
        public MessageContext (DbContextOptions options, ILogger<MessageContext> logger) : base (options) {
            _logger = logger;
            Database.EnsureCreated ();
        }
        public DbSet<MessageModel> Messages { get; set; }
    }
}