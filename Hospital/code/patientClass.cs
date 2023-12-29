using Newtonsoft.Json;

namespace hospitalSpace
{
    public class MedicineAssignedToThisPatient
    {
        public string name { get; set; }
        public double price { get; set; }

    }

    public class ExamsScheduled
    {
        public string name { get; set; }
        public string date_picked { get; set; }
        public string doctor_assigned { get; set; }
        public string area { get; set; }
    }

    public class AppointmentsScheduled
    {
        public string name { get; set; }
        public string date_picked { get; set; }
        public string doctor_assigned { get; set; }
        public string area { get; set; }
    }

    public class RandomPeople
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Date of Birth")]
        public string DateofBirth { get; set; }

        [JsonProperty("Healthcare number")]
        public int Healthcarenumber { get; set; }

        public string Image { get; set; }

        [JsonProperty("Phone Number")]
        public int Phonenumber { get; set; }

        [JsonProperty("Blood Type")]
        public string Bloodtype { get; set; }

        [JsonProperty("Identity number")]
        public long Identitynumber { get; set; }

        [JsonProperty("Medicine assigned to this patient")]
        public List<MedicineAssignedToThisPatient> Medicineassignedtothispatient { get; set; }

        [JsonProperty("Exams")]
        public List<string> ExamRecords { get; set; }

        [JsonProperty("Exams Scheduled")]
        public List<ExamsScheduled> examsScheduled { get; set; }

        [JsonProperty("Appointments Scheduled")]
        public List<AppointmentsScheduled> appointmentsScheduled { get; set; }
    }

    class PatientLog
    {
        public bool PatientInfo(int healthcarenumber, out RandomPeople selectedPatient)
        {
            ReadJson jsonReader = new ReadJson();
            string filePath = "Contents/Resources/randompeople.json";
            List<RandomPeople> patients = jsonReader.ReadPersonsFromJson(filePath);

            selectedPatient = null;

            foreach (var patient in patients)
            {
                if (patient.Healthcarenumber == healthcarenumber)
                {
                    selectedPatient = patient;
                    break;
                }
            }

            if (selectedPatient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PatientInfo2(long identitynumber, out RandomPeople selectedPatient)
        {
            ReadJson jsonReader = new ReadJson();
            string filePath = "Contents/Resources/randompeople.json";
            List<RandomPeople> patients = jsonReader.ReadPersonsFromJson(filePath);

            selectedPatient = null;

            foreach (var patient in patients)
            {
                if (patient.Identitynumber == identitynumber)
                {
                    selectedPatient = patient;
                    break;
                }
            }

            if (selectedPatient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}