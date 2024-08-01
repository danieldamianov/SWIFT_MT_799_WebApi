using ApplicationLogic.Models;
using ApplicationLogic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SWIFT_MT799_WebApi.Controllers
{
    [ApiController]
    [Route("api/get-messages-from-sender")]
    public class GetMessagesFromSpecificSenderBankController : ControllerBase
    {

        private readonly ILogger<GetMessagesFromSpecificSenderBankController> _logger;
        private readonly IMediator mediator;

        public GetMessagesFromSpecificSenderBankController
            (ILogger<GetMessagesFromSpecificSenderBankController> logger, IMediator mediator)
        {
            this._logger = logger;
            this.mediator = mediator;
        }

        // TODO:: Return IActionResult
        [HttpGet("{bankCode}", Name = "GetMessagesFromSpecificSenderBank")]
        public async Task<ICollection<SWIFT_MT799_Message_Model>> GetMessages(string bankCode)
        {
            ICollection<SWIFT_MT799_Message_Model> response = 
                await this.mediator.Send(new GetMessagesFromSpecificSenderBankQuery(bankCode));

            // TODO:: RETURN SOME IACTION RESULT
            return response;
        }
    }
}
