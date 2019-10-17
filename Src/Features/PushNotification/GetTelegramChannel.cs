using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Src.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Src.Features.PushNotification
{
    public class GetTelegramChannel
    {
        public class Query : IRequest<Result>
        {
            public string Name { get; set; }
        }

        public class Result
        {
            public string Id { get; set; }
        }

        public class GetTelegramChannelValidator : AbstractValidator<Query>
        {
            public GetTelegramChannelValidator()
            {
                RuleFor(query => query.Name).NotEmpty();
            }
        }

        public class SendTelegramMessageHandler : IRequestHandler<Query, Result>
        {
            private readonly IOptions<NotificationOptions> _options;

            public SendTelegramMessageHandler(IOptions<NotificationOptions> options)
            {
                _options = options;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var channel = GetChannel(request.Name);

                if (channel is null)
                {
                    throw new ArgumentNullException($"Channel was not found");
                }

                var result = new Result
                {
                    Id = channel.Id
                };

                return result;
            }

            private NotificationChannel GetChannel(string name)
            {
                var result = _options.Value.Channels.FirstOrDefault(channel => string.Equals(channel.Name, name, StringComparison.InvariantCultureIgnoreCase));

                return result;
            }
        }
    }
}
