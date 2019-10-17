using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Src.Features.Weather;

namespace Src.Controllers
{
    [Route("/weather")]
    public class WeatherController : Controller
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            var query = new GetWeather.Query();

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
