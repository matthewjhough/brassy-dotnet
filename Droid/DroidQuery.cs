using System.Collections.Generic;
using brassy_api.Operations;
using GraphQL.Types;

namespace brassy_api.Droid {
    public class DroidQuery : ObjectGraphType {
        public DroidQuery () {
            Field<DroidType> (
                "droid",
                resolve : context => new DroidModel { Id = 1, Name = "R2-D2" }
            );
        }
    }
}