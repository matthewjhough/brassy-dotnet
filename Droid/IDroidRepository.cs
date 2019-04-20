using System.Threading.Tasks;

namespace brassy_api.Droid {
    public interface IDroidRepository {
        Task<DroidModel> Get (int id);
    }
}