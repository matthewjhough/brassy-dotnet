using System.Threading.Tasks;

namespace brassy_api.src.Droid {
    public interface IDroidRepository {
        Task<DroidModel> Get (int id);
    }
}