using System.Linq;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Message {
    public static class MessageSeedData {
        public static void EnsureSeedData (this MessageContext db) {
            db._logger.LogInformation ("Seeding Database");
            if (!db.Messages.Any ()) {
                db._logger.LogInformation ("Seeding Messages");
                var message = new MessageModel { Id = "zfs0-2adf", Content = "Test Seed Message", SessionId = "123", UserId = "1" };
                db.Messages.Add (message);
                db.SaveChanges ();
            }
        }
    }
}