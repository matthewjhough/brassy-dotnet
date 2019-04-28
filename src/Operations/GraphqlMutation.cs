using System.Text;

namespace brassy_api.src.Operations {
    public class GraphQLMutation {
        public string OperationName { get; set; }
        public string NamedMutation { get; set; }
        public string Mutation { get; set; }
        public string Variables { get; set; }

        public override string ToString () {
            var builder = new StringBuilder ();
            builder.AppendLine ();
            if (!string.IsNullOrWhiteSpace (OperationName)) {
                builder.AppendLine ($"OperationName = {OperationName}");
            }
            if (!string.IsNullOrWhiteSpace (NamedMutation)) {
                builder.AppendLine ($"NamedMutation = {NamedMutation}");
            }
            if (!string.IsNullOrWhiteSpace (Mutation)) {
                builder.AppendLine ($"Mutation = {Mutation}");
            }
            if (!string.IsNullOrWhiteSpace (Variables)) {
                builder.AppendLine ($"Variables = {Variables}");
            }

            return builder.ToString ();
        }
    }
}