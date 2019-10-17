using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;
using Src.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Src.Features.Speaker
{
    public class SpeakSonos
    {
        public class Command : IRequest<Result>
        {
            public ICollection<Speaker> Speakers { get; set; }
            public string SpeakText { get; set; }
            public string LangCode { get; set; }
        }

        public class Speaker
        {
            public string Name { get; set; }
            public int Volume { get; set; }
        }

        public class Result
        {

        }

        public class SpeakSonosValidator : AbstractValidator<Command>
        {
            public SpeakSonosValidator()
            {
                RuleFor(cmd => cmd.Speakers).NotEmpty();
                RuleFor(cmd => cmd.SpeakText).NotEmpty();
                RuleFor(cmd => cmd.LangCode).NotEmpty();
            }
        }

        public class SpeakSonosHandler : IRequestHandler<Command, Result>
        {
            private readonly IOptions<SonosOptions> _options;

            public SpeakSonosHandler(IOptions<SonosOptions> options)
            {
                _options = options;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                foreach (var speaker in request.Speakers)
                {
                    RequestSonos(speaker, request.SpeakText, request.LangCode);
                }

                var result = new Result();

                return result;
            }

            private async void RequestSonos(Speaker speaker, string speakText, string langCode)
            {
                var client = new RestClient(_options.Value.ApiUrl);
                var request = new RestRequest("{speaker}/say/{speakText}/{langCode}/{volume}", Method.GET);
                request.AddUrlSegment("speaker", speaker.Name);
                request.AddUrlSegment("speakText", speakText);
                request.AddUrlSegment("langCode", langCode);
                request.AddUrlSegment("volume", speaker.Volume);

                client.ExecuteAsync(request, _ => { });
            }
        }
    }
}
