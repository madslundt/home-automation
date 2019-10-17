using System;

namespace Src.Extensions
{
    public static class TimeAgoExtension
    {
        public static string TimeAgo(this DateTime dt)
        {
            TimeSpan span = dt - DateTime.Now;

            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return $"om {years} år";
            }

            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("om {0} {1}", months, months == 1 ? "måned" : "måneder");
            }

            if (span.Days > 0)
                return String.Format("om {0} {1}", span.Days, span.Days == 1 ? "dag" : "dage");

            if (span.Hours > 0)
                return String.Format("om {0} {1}", span.Hours, span.Hours == 1 ? "time" : "timer");

            if (span.Minutes > 0)
                return String.Format("om {0} {1}", span.Minutes, span.Minutes == 1 ? "minut" : "minutter");

            if (span.Seconds > 5)
                return String.Format("om {0}", span.Seconds);

            if (span.Seconds <= 5)
                return "nu her";

            return string.Empty;
        }
    }
}
