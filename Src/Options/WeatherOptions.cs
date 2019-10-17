using System.ComponentModel.DataAnnotations;

namespace Src.Options
{
    public class WeatherOptions
    {
        [Required]
        public string ApiKey { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
