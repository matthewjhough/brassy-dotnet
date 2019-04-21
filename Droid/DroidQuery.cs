using System.Collections.Generic;
using brassy_api.Operations;
using GraphQL.Types;

namespace brassy_api.Droid {
    public class DroidQuery : ObjectGraphType {
        public DroidQuery (IDroidRepository _droidRepository) {
            Field<DroidType> (
                "droid",
                resolve : context => _droidRepository.Get (1)
            );
        }
    }
}