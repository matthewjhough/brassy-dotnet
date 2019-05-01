using System.Threading.Tasks;
using brassy_api.src.Message;
using brassy_api.src.Operations;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace brassy_api.src.Controllers {

    [Route ("graphql")]
    public class GraphQLController : Controller {
        private readonly ILogger _logger;
        private IMessageRepository _messageRepository;
        public GraphQLController (
            IMessageRepository messageRepository,
            ILogger<GraphQLController> logger
        ) {
            _logger = logger;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] GraphQLQuery query) {
            MessageSchema schema = new MessageSchema (_messageRepository);

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
            _logger.LogInformation ("Received request for GraphiQL. Sending GUI back");
            return View ();
        }
    }

}