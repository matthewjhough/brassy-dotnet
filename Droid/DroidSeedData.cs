using System.Linq;

namespace brassy_api.Droid {
    public static class DroidSeedData {
        public static void EnsureSeedData (this DroidContext db) {
            if (!db.Droids.Any ()) {
                var droid = new DroidModel { Name = "R2-D2" };
                db.Droids.Add (droid);
                db.SaveChanges ();
            }
        }
    }
}