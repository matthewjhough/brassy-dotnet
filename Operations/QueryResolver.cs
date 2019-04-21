using brassy_api.Droid;
using GraphQL.Types;

namespace brassy_api.Operations {
    public class QueryResolver : ObjectGraphType {

        public QueryResolver (IDroidRepository _droidRepository) {
            Field<DroidType> (
                "droid",
                resolve : context => _droidRepository.Get (1)
            );
        }
    }
}