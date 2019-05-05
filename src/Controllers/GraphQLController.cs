using System;
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
        private IDocumentExecuter _documentExecuter { get; set; }
        public GraphQLController (
            IDocumentExecuter documentExecuter,
            IMessageRepository messageRepository,
            ILogger<GraphQLController> logger
        ) {
            _documentExecuter = documentExecuter;
            _logger = logger;
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public IActionResult Index () {
            _logger.LogInformation ("Got request for GraphiQL. Sending GUI back");
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] GraphQLQuery query) {
            MessageSchema schema = new MessageSchema (_messageRepository);

            if (query == null) { throw new ArgumentNullException (nameof (query)); }
            var executionOptions = new ExecutionOptions { Schema = schema, Query = query.Query };

            try {
                var result = await _documentExecuter.ExecuteAsync (executionOptions).ConfigureAwait (false);

                if (result.Errors?.Count > 0) {
                    _logger.LogError ("GraphQL errors: {0}", result.Errors);
                    return BadRequest (result);
                }

                _logger.LogDebug ("GraphQL execution result: {result}", JsonConvert.SerializeObject (result.Data));
                return Ok (result);
            } catch (Exception ex) {
                _logger.LogError ("Document exexuter exception", ex);
                return BadRequest (ex);
            }
        }

    }

}