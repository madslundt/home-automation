using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;
using Src.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Src.Features.Speaker
{
    public class SpeakCast
    {
        public class Command : IRequest<Result>
        {
            public string Message { get; set; }
        }

        public class Result
        {

        }

        public class SpeakCastValidator : AbstractValidator<Command>
        {
            public SpeakCastValidator()
            {
                RuleFor(cmd => cmd.Message).NotEmpty();
            }
        }

        public class SpeakCastHandler : IRequestHandler<Command, Result>
        {
            private readonly IOptions<CastOptions> _options;

            public SpeakCastHandler(IOptions<CastOptions> options)
            {
                _options = options;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                Broadcast(request.Message);

                var result = new Result();

                return result;
            }

            private void Broadcast(string message)
            {
                var client = new RestClient(_options.Value.ApiUrl);
                var request = new RestRequest("assistant/broadcast", Method.POST);
                request.AddJsonBody(new
                {
                    message
                });

                client.ExecuteAsync(request, _ => { });
            }
        }
    }
}
