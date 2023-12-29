using Newtonsoft.Json;
using System.Diagnostics;

namespace hospitalSpace;

public partial class ScheduleAppointments : ContentPage
{
    public RandomPeople SelectedPatient { get; set; }

    public ScheduleAppointments()
	{
		InitializeComponent();
    }

    private List<Staff> staff;

    public ScheduleAppointments(RandomPeople patient) : this()
    {
        Console.WriteLine("starting json read...");
        SelectedPatient = patient;

        Task.Run(async() =>
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
   
                foreach (var person in staff)
                {
                    string area = person.Specialty;
                    uniqueAreas.Add(area);
                }

                List<string> areaList = uniqueAreas.ToList();

                await ListViewofAreas.Dispatcher.DispatchAsync(() => //idk why i needed to do this but the program was crashing so the task run is used
                                                                     //to run the readjson on a backgroung thread, this updates the ui on the main thread
                {
                    ListViewofAreas.ItemsSource = areaList;
                });
            }
        });

        ListViewofAreas.ItemSelected += ListViewofAreas_itemselected;
    }

    private void ListViewofAreas_itemselected(object sender, SelectedItemChangedEventArgs e)
    {
        string selectedItem = e.SelectedItem as string;
        List<string> listdocs = new List<string>();

        if (selectedItem != null)
        {
            foreach (var person in staff)
            {
                if (selectedItem == person.Specialty)
                {
                    if (person.Occupation == "Doctor")
                    {
                        listdocs.Add(person.Name);
                    }
                }
            }
            ListViewofDocs.ItemsSource = listdocs;
        }
    }



    private async void RegisterClicked(object sender, EventArgs e)
    {
        try
        {
            string selectedDoctorName = ListViewofDocs.SelectedItem as string;
            DateTime selectedDate = AppointmentDatePicker.Date;
            TimeSpan selectedTime = Pickingtime.Time;
            DateTime fulldate = selectedDate.Date + selectedTime;
            string selectedArea = ListViewofAreas.SelectedItem as string;

            Staff selectedDoctor = staff.FirstOrDefault(doc => doc.Name == selectedDoctorName);

            if (selectedTime >= selectedDoctor.WorkingHours.StartTime && selectedTime <= selectedDoctor.WorkingHours.EndTime)
            {
                foreach (var appointment in SelectedPatient.appointmentsScheduled)
                {
                    string area = appointment.area;
                    string doctor_assigned = appointment.doctor_assigned;
                    string date_picked = appointment.date_picked;
                }

                AppointmentsScheduled newAppointment = new AppointmentsScheduled
                {
                    name = "Appointment",
                    date_picked = fulldate.ToString(),
                    doctor_assigned = selectedDoctorName,
                    area = selectedArea,
                };

                //add the new patient to the list.
                SelectedPatient.appointmentsScheduled.Add(newAppointment);

                //read the existing list of patients from the json file
                var jsonFilePath = "Contents/Resources/randompeople.json";
                var jsonData = File.ReadAllText(jsonFilePath);
                var patientsList = JsonConvert.DeserializeObject<List<RandomPeople>>(jsonData) ?? new List<RandomPeople>();

                var patientToUpdate = patientsList.FirstOrDefault(p => p.Healthcarenumber == SelectedPatient.Healthcarenumber);
                if (patientToUpdate != null)
                {
                    patientToUpdate.appointmentsScheduled = SelectedPatient.appointmentsScheduled;
                }

                //serializing the updated list back to the json file
                jsonData = JsonConvert.SerializeObject(patientsList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);

                //inform the user that the registration was successful
                await DisplayAlert("Success", "The appointment has been registered.", "OK");
                await Navigation.PopAsync();
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
            Console.WriteLine($"Exception caught: {ex}");
            await DisplayAlert("Error", "An error occurred while processing the registration.", "OK");
            return;
        }
    }
}
