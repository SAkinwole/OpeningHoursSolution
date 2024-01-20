namespace OpeningHoursSolution.Models
{
    public class OpeningHoursRequest
    {
        public List<OpeningHours> Monday { get; set; }
        public List<OpeningHours> Tuesday { get; set; }
        public List<OpeningHours> Wednesday { get; set; }
        public List<OpeningHours> Thursday { get; set; }
        public List<OpeningHours> Friday { get; set; }
        public List<OpeningHours> Saturday { get; set; }
        public List<OpeningHours> Sunday { get; set; }
    }
}
