using GraphQL.Types;

namespace brassy_api.src.Droid {
    public class DroidType : ObjectGraphType<DroidModel> {
        public DroidType () {
            Field (x => x.Id).Description ("The Id of the Droid.");
            Field (x => x.Name, nullable : true).Description ("The name of the Droid.");
        }
    }
}