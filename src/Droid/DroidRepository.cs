using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace brassy_api.src.Droid {
    public class DroidRepository : IDroidRepository {
        private DroidContext _db { get; set; }
        private readonly ILogger _logger;
        public DroidRepository (DroidContext db, ILogger<DroidRepository> logger) {
            _db = db;
            _logger = logger;
        }
        public Task<DroidModel> Get (int id) {
            _logger.LogInformation ("Get droid with id = {id}", id);
            return _db.Droids.FirstOrDefaultAsync (droid => droid.Id == id);
        }
    }
}