using brassy_api.src.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Data {
    public class BrassyContext : DbContext {
        public readonly ILogger _logger;
        public BrassyContext (DbContextOptions options, ILogger<BrassyContext> logger) : base (options) {
            _logger = logger;
            Database.EnsureCreated ();
        }
        public DbSet<MessageModel> Messages { get; set; }
    }
}