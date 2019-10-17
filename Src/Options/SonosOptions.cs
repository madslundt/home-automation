using System.ComponentModel.DataAnnotations;

namespace Src.Options
{
    public class SonosOptions
    {
        [Required]
        public string ApiUrl { get; set; }
    }
}
