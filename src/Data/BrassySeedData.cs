using System;
using System.Linq;
using brassy_api.src.Message;
using brassy_api.src.Mood;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Data {
    public static class BrassySeedData {
        public static void EnsureSeedData (this BrassyContext db) {
            db._logger.LogInformation ("Seeding Database");
            if (!db.Messages.Any ()) {
                db._logger.LogInformation ("Seeding Messages");
                long dateticks = DateTime.Now.Ticks;
                long datemilliseconds = dateticks / TimeSpan.TicksPerMillisecond;

                var message = new MessageModel {
                    Id = Guid.NewGuid ().ToString (),
                    Content = "Test Seed Message",
                    Mood = MoodModel.ENGAGED,
                    CreatedAt = datemilliseconds
                };

                db.Messages.Add (message);
                db.SaveChanges ();
            }
        }
    }
}