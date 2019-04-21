using System.Threading.Tasks;
using brassy_api.Operations;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace brassy_api.Controllers {

    [Route ("graphql")]
    public class GraphQLController : Controller {
        private readonly ILogger _logger;
        private QueryResolver _queryResolver { get; set; }
        public GraphQLController (QueryResolver queryResolver, ILogger<GraphQLController> logger) {
            _logger = logger;
            _queryResolver = queryResolver;
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] GraphQLQuery query) {
            var schema = new Schema { Query = _queryResolver };

            var result = await new DocumentExecuter ().ExecuteAsync (_ => {
                _.Schema = schema;
                _.Query = query.Query;

            }).ConfigureAwait (false);

            if (result.Errors?.Count > 0) {
                _logger.LogError ("GraphQL errors: {0}", result.Errors);
                return BadRequest (result);
            }

            _logger.LogDebug ("GraphQL execution result: {result}", JsonConvert.SerializeObject (result.Data));

            return Ok (result);
        }

        [HttpGet]
        public IActionResult Index () {
            _logger.LogInformation ("Got request for GraphiQL. Sending GUI back");
            return View ();
        }
    }

}