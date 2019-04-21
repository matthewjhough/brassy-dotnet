using System.Linq;
using brassy_api.src.Message;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Data {
    public static class BrassySeedData {
        public static void EnsureSeedData (this BrassyContext db) {
            db._logger.LogInformation ("Seeding Database");
            if (!db.Messages.Any ()) {
                db._logger.LogInformation ("Seeding Messages");

                var message = new MessageModel {
                    Id = "zfs0-2adf",
                    Content = "Test Seed Message",
                    SessionId = "123",
                    UserId = "1"
                };

                db.Messages.Add (message);
                db.SaveChanges ();
            }
        }
    }
}