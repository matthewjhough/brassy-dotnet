using Microsoft.EntityFrameworkCore;

namespace brassy_api.Droid {
    public class DroidContext : DbContext {
        public DroidContext (DbContextOptions options) : base (options) {
            Database.EnsureCreated ();
        }

        public DbSet<DroidModel> Droids { get; set; }
    }
}