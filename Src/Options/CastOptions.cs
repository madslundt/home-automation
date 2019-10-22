using System.ComponentModel.DataAnnotations;

namespace Src.Options
{
    public class CastOptions
    {
        [Required]
        public string ApiUrl { get; set; }
    }
}
