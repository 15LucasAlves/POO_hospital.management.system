using System;
using Newtonsoft.Json;

namespace hospitalSpace
{
    public class WorkingHours
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        public TimeSpan StartTime => TimeSpan.Parse(Start);
        public TimeSpan EndTime => TimeSpan.Parse(End);
    }

    public class Staff
    {
        [JsonProperty("Staff Name")]
        public string Name { get; set; }

        [JsonProperty("Date of Birth")]
        public string DateofBirth { get; set; }

        [JsonProperty("Identity Number")]
        public long IdentityNumber { get; set; }

        [JsonProperty("Occupation")]
        public string Occupation { get; set; }

        [JsonProperty("Specialty")]
        public string Specialty { get; set; }

        [JsonProperty("Staff Number")]
        public int StaffNumber { get; set; }

        [JsonProperty("Private Number")]
        public string PrivateNumber { get; set; }

        [JsonProperty("Working Hours")]
        public WorkingHours WorkingHours { get; set; }
    }
}