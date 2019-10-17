using DarkSky.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Src.Extensions;
using Src.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Src.Features.Weather
{
    public class GetWeather
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public Weather Currently { get; set; }
            public Weather Hourly { get; set; }
            public Weather Daily { get; set; }
        }

        public class Weather
        {
            public double PrecipProbability { get; set; }
            public DateTime? Time { get; set; }
            public string TimeTo { get { return Time?.TimeAgo(); } }
            public string Icon { get; set; }
            public string Summary { get; set; }
        }


        public class GetWeatherHandler : IRequestHandler<Query, Result>
        {
            private readonly IOptions<WeatherOptions> _options;

            public GetWeatherHandler(IOptions<WeatherOptions> options)
            {
                _options = options;
            }
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var forecast = await GetForecast();

                var result = new Result
                {
                    Currently = GetWeatherData(forecast.Currently),
                    Hourly = GetWeatherData(forecast.Hourly),
                    Daily = GetWeatherData(forecast.Daily),
                };

                return result;
            }

            private async Task<Forecast> GetForecast()
            {
                var darkSky = new DarkSky.Services.DarkSkyService(_options.Value.ApiKey);
                var forecastResponse = await darkSky.GetForecast(_options.Value.Latitude, _options.Value.Longitude, new OptionalParameters
                {
                    MeasurementUnits = _options.Value.Unit,
                    LanguageCode = _options.Value.LanguageCode
                });

                if (!forecastResponse.IsSuccessStatus)
                {
                    throw new Exception(forecastResponse.ResponseReasonPhrase);
                }

                return forecastResponse.Response;
            }

            private Weather GetWeatherData(DataPoint dataPoint)
            {
                var result = new Weather
                {
                    Icon = dataPoint.Icon.ToString(),
                    PrecipProbability = dataPoint.PrecipProbability ?? 0.0,
                    Summary = dataPoint.Summary,
                    Time = dataPoint.DateTime.DateTime
                };

                return result;
            }

            private Weather GetWeatherData(DataBlock dataBlock)
            {
                var upcoming = dataBlock.Data.FirstOrDefault(d => d.Icon.ToString() == dataBlock.Icon.ToString());

                var result = new Weather
                {
                    Icon = dataBlock.Icon.ToString(),
                    PrecipProbability = upcoming?.PrecipProbability ?? 0.0,
                    Summary = dataBlock.Summary,
                    Time = upcoming?.DateTime.DateTime
                };

                return result;
            }
        }
    }
}
