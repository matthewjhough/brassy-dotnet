using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace brassy_api.Droid {
    public class DroidRepository : IDroidRepository {
        private DroidContext _db { get; set; }
        public DroidRepository (DroidContext db) {
            _db = db;
        }
        public Task<DroidModel> Get (int id) {
            return _db.Droids.FirstOrDefaultAsync (droid => droid.Id == id);
        }
    }
}