using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Src.Options
{
    public class NotificationOptions
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public IEnumerable<NotificationChannel> Channels { get; set; }
    }

    public class NotificationChannel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
