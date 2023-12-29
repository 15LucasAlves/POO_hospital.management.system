using Newtonsoft.Json;

namespace hospitalSpace;

public partial class ScheduleExams : ContentPage
{
    public RandomPeople SelectedPatient { get; set; }

    public ScheduleExams()
	{
		InitializeComponent();
	}

    private List<Staff> staff;

    public ScheduleExams(RandomPeople patient) : this()
    {
        SelectedPatient = patient;

        Task.Run(async () =>
        {
            ReadJsonStaff jsonReader = new ReadJsonStaff();
            string filePath = "Contents/Resources/generated_staff.json";
            staff = jsonReader.ReadStaffFromJson(filePath);

            if (staff == null)
            {
                await DisplayAlert("Error", "Error loading the files or data.", "OK");
            }
            else
            {

                HashSet<string> uniqueAreas = new HashSet<string>(); // hashset to keep unique values, throws automatically out repeated values

                Console.WriteLine("json read...");
                foreach (var person in staff)
                {
                    string area = person.Specialty;
                    uniqueAreas.Add(area);
                }

                List<string> areaList = uniqueAreas.ToList();

                await ListViewofAreas1.Dispatcher.DispatchAsync(() => //idk why i needed to do this but the program was crashing so the task run is used
                                                                      //to run the readjson on a backgroung thread, this updates the ui on the main thread
                {
                    ListViewofAreas1.ItemsSource = areaList;
                });
            }
        });
        ListViewofAreas1.ItemSelected += ListViewofAreas1_itemselected;
    }

    private void ListViewofAreas1_itemselected(object sender, SelectedItemChangedEventArgs e)
    {
        string selectedItem = e.SelectedItem as string;
        List<string> listdocs1 = new List<string>();

        if (selectedItem != null)
        {
            foreach (var person in staff)
            {
                if (selectedItem == person.Specialty)
                {
                    if (person.Occupation == "Doctor")
                    {
                        listdocs1.Add(person.Name);
                    }
                }
            }
            ListViewofDocs1.ItemsSource = listdocs1;
        }
    }

    private async void RegisterExamClicked(object sender, EventArgs e)
    {
        try
        {
            string selectedDoctorName = ListViewofDocs1.SelectedItem as string;
            DateTime selectedDate = ExamsDatePicker.Date;
            TimeSpan selectedTime = ExamsTimePicker.Time;
            DateTime fulldate = selectedDate.Date + selectedTime;
            string selectedArea = ListViewofAreas1.SelectedItem as string;

            //altered, iterated through the list to find the info of the doc
            Staff selectedDoctor = staff.FirstOrDefault(doc => doc.Name == selectedDoctorName);

            if (selectedTime >= selectedDoctor.WorkingHours.StartTime && selectedTime <= selectedDoctor.WorkingHours.EndTime)
            {
                foreach (var examsscheduled in SelectedPatient.appointmentsScheduled)
                {
                    string area = examsscheduled.area;
                    string doctor_assigned = examsscheduled.doctor_assigned;
                    string date_picked = examsscheduled.date_picked;
                }

                ExamsScheduled newExam = new ExamsScheduled
                {
                    name = "Exam",
                    date_picked = fulldate.ToString(),
                    doctor_assigned = selectedDoctorName,
                    area = selectedArea,
                };

                //add the new patient to the list.
                SelectedPatient.examsScheduled.Add(newExam);

                //read the existing list of patients from the json file
                var jsonFilePath = "Contents/Resources/randompeople.json";
                if (File.Exists(jsonFilePath))
                {
                    var jsonData = File.ReadAllText(jsonFilePath);
                    var patientsList = JsonConvert.DeserializeObject<List<RandomPeople>>(jsonData) ?? new List<RandomPeople>();

                    var patientToUpdate = patientsList.FirstOrDefault(p => p.Healthcarenumber == SelectedPatient.Healthcarenumber);
                    if (patientToUpdate != null)
                    {
                        patientToUpdate.examsScheduled = SelectedPatient.examsScheduled;
                    }

                    //serializing the updated list back to the json file
                    jsonData = JsonConvert.SerializeObject(patientsList, Formatting.Indented);
                    File.WriteAllText(jsonFilePath, jsonData);

                    //inform the user that the registration was successful
                    await DisplayAlert("Success", "The exam has been registered.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Error loading the files or data.", "OK");
                }
            }
            else
            {
                // The appointment time is outside the doctor's working hours, so inform the user
                await DisplayAlert("Error", "Unavailable slot. Doctor is off schedule.", "OK");
                return;
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while processing the registration. " + $"{ex.Message}", "OK");
            return;
        }
    }
}


