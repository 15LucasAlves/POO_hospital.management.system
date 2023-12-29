namespace hospitalSpace;

public partial class PatientFound : ContentPage
{
    public RandomPeople SelectedPatient { get; set; }

    public PatientFound()
    {
        InitializeComponent();
    }
    public PatientFound(RandomPeople patient) : this()
    {
        SelectedPatient = patient;
        NameLabel.Text = "Name: " + SelectedPatient.Name;
        DateofBirthLabel.Text = "Date of Birth: " + SelectedPatient.DateofBirth;
        PhonenumberLabel.Text = "Phone number: " + SelectedPatient.Phonenumber.ToString();
        HealthcarenumberLabel.Text = "Healthcare number: " + SelectedPatient.Healthcarenumber.ToString();
        IdentityLabel.Text = "Identity number: " + SelectedPatient.Identitynumber.ToString();
        BloodtypeLabel.Text = "Blood Type: " + SelectedPatient.Bloodtype;
    }

    private async void ScheduleAppointmentClicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ScheduleAppointments(SelectedPatient));
    }
    private async void MedicalHistoryClicked(System.Object sender, System.EventArgs e)
    {
        //don't forget the constructor that accepts random people object so u can access the patient choosen
        await Navigation.PushAsync(new MedicalHist(SelectedPatient));
    }
    private async void ExamsClicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ScheduleExams(SelectedPatient));
    }
}