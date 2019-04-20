using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace brassy_api.Droid {
    public class DroidRepository : IDroidRepository {
        private List<DroidModel> _droids = new List<DroidModel> {
            new DroidModel { Id = 1, Name = "R2-D2" }
        };
        public Task<DroidModel> Get (int id) {
            return Task.FromResult (_droids.FirstOrDefault (droid => droid.Id == id));
        }
    }
}