using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Src.Features.Speaker;

namespace Src.Controllers
{
    [Route("/speakers")]
    public class SpeakerController : Controller
    {
        private readonly IMediator _mediator;

        public SpeakerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("announce-sonos")]
        public async Task<IActionResult> AnnounceSonos([FromBody] object request)
        {
            SpeakSonos.Command command;
            if (request is string)
            {
                command = JsonConvert.DeserializeObject<SpeakSonos.Command>(request.ToString());
            }
            else
            {
                command = (request as JObject).ToObject<SpeakSonos.Command>();
            }

            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost, Route("announce-cast")]
        public async Task<IActionResult> AnnounceCast([FromBody] object request)
        {
            SpeakCast.Command command;
            if (request is string)
            {
                command = JsonConvert.DeserializeObject<SpeakCast.Command>(request.ToString());
            }
            else
            {
                command = (request as JObject).ToObject<SpeakCast.Command>();
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
