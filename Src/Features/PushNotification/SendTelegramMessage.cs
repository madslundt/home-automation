using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Src.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Src.Features.PushNotification
{
    public class SendTelegramMessage
    {
        public class Command : IRequest<Result>
        {
            public string Text { get; set; }

            public string Name { get; set; }
        }

        public class Result
        {

        }

        public class SendTelegramMessageValidator : AbstractValidator<Command>
        {
            public SendTelegramMessageValidator()
            {
                RuleFor(cmd => cmd.Text).NotEmpty();
                RuleFor(cmd => cmd.Name).NotEmpty();
            }
        }

        public class SendTelegramMessageHandler : IRequestHandler<Command, Result>
        {
            private readonly IMediator _mediator;
            private readonly IOptions<NotificationOptions> _options;

            public SendTelegramMessageHandler(IMediator mediator, IOptions<NotificationOptions> options)
            {
                _mediator = mediator;
                _options = options;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var channel = await _mediator.Send(new GetTelegramChannel.Query
                {
                    Name = request.Name
                });

                if (channel is null)
                {
                    throw new ArgumentNullException($"{nameof(channel)} was not found");
                }

                var botClient = new TelegramBotClient(_options.Value.AccessToken);

                await botClient.SendTextMessageAsync(channel.Id, request.Text);

                return new Result();
            }
        }
    }
}
