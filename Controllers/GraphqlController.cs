using System.Threading.Tasks;
using brassy_api.Droid;
using brassy_api.Operations;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace brassy_api.Controllers {

    [Route ("graphql")]
    public class GraphQLController : Controller {
        private readonly ILogger _logger;
        private DroidQuery _droidQuery { get; set; }
        public GraphQLController (DroidQuery droidQuery, ILogger<GraphQLController> logger) {
            _logger = logger;
            _droidQuery = droidQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] GraphQLQuery query) {
            var schema = new Schema { Query = _droidQuery };

            var result = await new DocumentExecuter ().ExecuteAsync (_ => {
                _.Schema = schema;
                _.Query = query.Query;

            }).ConfigureAwait (false);

            if (result.Errors?.Count > 0) {
                _logger.LogError ($"[:::Controller:::] Failed: {result.Query}");
                _logger.LogError ($"[:::Controller:::] Error: {result.Errors.AsDictionary()}");
                return BadRequest ();
            }

            return Ok (result);
        }
    }
}