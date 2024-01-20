using OpeningHoursSolution.Models;

namespace OpeningHoursSolution
{
    public class OpeningHoursService
    {
        public Dictionary<string, object> FormatOpeningHours(OpeningHoursRequest request)
        {
            var formattedHours = new Dictionary<string, object>();

            foreach (var dayOfWeek in Enum.GetNames(typeof(DayOfWeek)))
            {
                var day = dayOfWeek.ToLower();

                if (!request.GetType().GetProperty(day).GetValue(request, null).Equals(null))
                {
                    var openingHours = request.GetType().GetProperty(day).GetValue(request, null) as List<OpeningHours>;

                    if (openingHours == null || !openingHours.Any())
                    {
                        formattedHours.Add(day, "Closed");
                    }
                    else
                    {
                        formattedHours.Add(day, openingHours.SelectMany(FormatHours).ToList());
                    }
                }
            }
            return formattedHours;
        }

        

        private IEnumerable<string> FormatHours(OpeningHours openingHours)
        {
            var currentDay = openingHours.Type == "open" ? openingHours.Value : (int?)null;
            var formattedHours = new List<string>();

            if (currentDay != null)
            {
                foreach (var entry in openingHours.Type == "open" ? openingHours.Value : (openingHours.Value + 86399) % 86400)
                {
                    if (entry.Type == "open")
                    {
                        currentDay = entry.Value;
                    }
                    else if (entry.Type == "close" && currentDay != null)
                    {
                        formattedHours.Add(ConvertTo12HourClock(currentDay.Value, entry.Value));
                        currentDay = null;
                    }
                }
            }

            return formattedHours;
        }
        private string ConvertTo12HourClock(int openTime, int closeTime)
        {
            var formattedOpenTime = DateTimeOffset.FromUnixTimeSeconds(openTime).ToLocalTime().ToString("hh:mm tt");
            var formattedCloseTime = DateTimeOffset.FromUnixTimeSeconds(closeTime).ToLocalTime().ToString("hh:mm tt");
            return $"{formattedOpenTime} - {formattedCloseTime}";
        }
    }
}
