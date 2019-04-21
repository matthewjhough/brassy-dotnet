using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Droid {
    public class DroidContext : DbContext {
        public readonly ILogger _logger;
        public DroidContext (DbContextOptions options, ILogger<DroidContext> logger) : base (options) {
            _logger = logger;
            Database.EnsureCreated ();
        }
        public DbSet<DroidModel> Droids { get; set; }
    }
}