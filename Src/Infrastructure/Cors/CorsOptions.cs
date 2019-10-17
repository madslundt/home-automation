using System.Collections.Generic;

namespace Src.Infrastructure.Cors
{
    public class CorsOptions
    {
        public ICollection<string> Origins { get; set; } = new List<string>();
    }
}