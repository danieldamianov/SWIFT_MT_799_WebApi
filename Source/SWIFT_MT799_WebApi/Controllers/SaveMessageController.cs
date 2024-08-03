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

        private readonly IMediator mediator;

        public SaveMessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "SaveMessage")]
        [Consumes("text/plain")]
        public async Task<bool> Save()
        {
            // TODO::
            string message;
            
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                message = await reader.ReadToEndAsync();
            }

            bool respone = await this.mediator.Send(new SaveMessageCommand(message));

            // TODO:: RETURN SOME IACTION RESULT
            return respone;
        }
    }
}
