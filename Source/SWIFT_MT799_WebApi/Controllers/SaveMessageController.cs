using ApplicationLogic.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SWIFT_MT799_WebApi.Controllers
{
    [ApiController]
    [Route("api/save")]
    public class SaveMessageController : ControllerBase
    {

        private readonly ILogger<SaveMessageController> _logger;
        private readonly IMediator mediator;

        public SaveMessageController(ILogger<SaveMessageController> logger, IMediator mediator)
        {
            this._logger = logger;
            this.mediator = mediator;
        }

        [HttpPost(Name = "SaveMessage")]
        public async Task<bool> Save()
        {
            string message;

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                message = await reader.ReadToEndAsync();
            }

            bool respone = await this.mediator.Send(new SaveMessageCommand(message));
            return respone;
        }
    }
}
