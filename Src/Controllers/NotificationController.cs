using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Src.Features.PushNotification;
using System;
using System.Threading.Tasks;

namespace Src.Controllers
{
    [Route("/notifications")]
    public class NotificationController : Controller
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("send-telegram-notification")]
        public async Task<IActionResult> SendTelegramNotification([FromBody] object request)
        {
            SendTelegramMessage.Command command;
            if (request is string)
            {
                command = JsonConvert.DeserializeObject<SendTelegramMessage.Command>(request.ToString());
            }
            else
            {
                command = request as SendTelegramMessage.Command;
            }

            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
