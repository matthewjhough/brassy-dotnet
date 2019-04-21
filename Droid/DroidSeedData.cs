using System.Linq;
using Microsoft.Extensions.Logging;

namespace brassy_api.Droid {
    public static class DroidSeedData {
        public static void EnsureSeedData (this DroidContext db) {
            db._logger.LogInformation ("Seeding Database");
            if (!db.Droids.Any ()) {
                db._logger.LogInformation ("Seeding Droids");
                var droid = new DroidModel { Name = "R2-D2" };
                db.Droids.Add (droid);
                db.SaveChanges ();
            }
        }
    }
}