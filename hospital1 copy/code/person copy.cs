using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace betahospital
{
    public class MedicineAssignedToThisPatient
    {
        public string name { get; set; }
        public double price { get; set; }

    }

        public class RandomPeople
    {
         public string Name { get; set; }

        [JsonProperty("Date of Birth")]
        public string DateofBirth { get; set; }

        [JsonProperty("Healthcare number")]
        public int Healthcarenumber { get; set; }
        public string Image { get; set; }

        [JsonProperty("Identity number")]
        public object Identitynumber { get; set; }

        [JsonProperty("Medicine assigned to this patient")]
        public MedicineAssignedToThisPatient Medicineassignedtothispatient { get; set; }
        public List<string> Exams { get; set; }
    }
}