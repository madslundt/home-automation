using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost, Route("announce")]
        public async Task<IActionResult> Announce([FromBody] SpeakSonos.Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
