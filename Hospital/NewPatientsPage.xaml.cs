using Newtonsoft.Json;
namespace hospitalSpace;

public partial class NewPatientsPage : ContentPage
{
    private PatientLog logg = new PatientLog();

    public NewPatientsPage(string HCN)
	{
		InitializeComponent();
        EntryHealthcareNumber.Text = HCN;
    }

    private async void RegisterButtonClicked(System.Object sender, System.EventArgs e)
    {
        //validation for the name field

       foreach(char letter in EntryName.Text)
        {
            if(char.IsDigit(letter) != false)
            {
                await DisplayAlert("Invalid Input", "Field only accepts alphabetic input.", "OK");
                return;
            }
        }
        Console.WriteLine("NameValidation");

        //validation for the blood types available since having a picker is not possible

        bool validbloodtype = false;
        string[] bloodtypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        int lengthblood = bloodtypes.Length;
        for (int i = 0; i < lengthblood; i++)
        {
            if(string.Compare(EntryBloodType.Text, bloodtypes[i]) == 0)
            {
                validbloodtype = true;
                break;
            }
        }
        if (!validbloodtype)
        {
            await DisplayAlert("Invalid Input", "Blood type does not correspond to an available type.", "OK");
            return;
        }

        Console.WriteLine("BloodValidation");

        //validation for the healthcare number

        string validation = EntryHealthcareNumber.Text;

        if (validation.Length != 6 || !validation.All(char.IsDigit))
        {
            await DisplayAlert("Invalid Input", "Healthcare number must be 6 digits.", "OK");
            return; // stop processing if validation fails
        }
        else
        {

            RandomPeople selectedPatient = null;

            //lambda function to match the user input in the search bar with the info from the .json file

            bool result = await Task.Run(() => logg.PatientInfo(int.Parse(EntryHealthcareNumber.Text), out selectedPatient));

            if (selectedPatient != null)
            {
                await DisplayAlert("Invalid input.", "Healthcare number already in use.", "OK");
                return;
            }
        }
        Console.WriteLine("HealthnumberValidation");

        //validation for identity number

        string validationID = EntryIdentityNumber.Text;

        if (validationID.Length != 10 || !validationID.All(char.IsDigit))
        {
            await DisplayAlert("Invalid Input", "Identity number must be 10 digits.", "OK");
            return; // stop processing if validation fails
        }
        else
        {
            RandomPeople selectedPatient = null;

            // Parse the identity number as a long instead of int
            if (long.TryParse(validationID, out long identityNumber))
            {
                bool result = await Task.Run(() => logg.PatientInfo2(identityNumber, out selectedPatient));
                Console.WriteLine("{0}", result);

                if (selectedPatient != null)
                {
                    await DisplayAlert("Invalid input.", "Identity number already in use.", "OK");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Invalid input.", "Identity number format is incorrect.", "OK");
                return;
            }
        }
        Console.WriteLine("identityValidation");

        //validation for phone number

        string validationphone = EntryPhoneNumber.Text;
        if(validationphone.Length != 9)
        {
            await DisplayAlert("Invalid input.", "Valid phone numbers have 9 digits.", "OK");
            return;
        }
 
            if(validationphone[0] != '9')
            {
                await DisplayAlert("Invalid input.", "All phone numbers should start with a '9'.", "OK");
                return;
            }
            if (validationphone[1] != '3' && validationphone[1] != '2' && validationphone[1] != '1' && validationphone[1] != '6')
            {
                await DisplayAlert("Invalid input.", "All phone numbers should start with a '91', '92', '93', '96'.", "OK");
                return;
            }

        Console.WriteLine("PhoneValidation");
        //create a new RandomPeople object 
        var newPatient = new RandomPeople
        {
            Name = EntryName.Text,
            DateofBirth = DatePickerDateOfBirth.Date.ToString("yyyy-MM-dd"),
            Healthcarenumber = int.Parse(EntryHealthcareNumber.Text),
            Identitynumber = long.Parse(EntryIdentityNumber.Text),
            Phonenumber = int.Parse(EntryPhoneNumber.Text),
            Bloodtype = EntryBloodType.Text,
            Medicineassignedtothispatient = new List<MedicineAssignedToThisPatient>(),
            ExamRecords = new List<string>(),
            examsScheduled = new List<ExamsScheduled>(),
            appointmentsScheduled = new List<AppointmentsScheduled>()

        };

        Console.WriteLine("Starting registration");
        var jsonFilePath = "Contents/Resources/randompeople.json";
        if (File.Exists(jsonFilePath))
        {
            try
            {
                //read the existing list of patients from the json file
                var jsonData = File.ReadAllText(jsonFilePath);
                var patientsList = JsonConvert.DeserializeObject<List<RandomPeople>>(jsonData) ?? new List<RandomPeople>();

                //add the new patient to the list.
                patientsList.Add(newPatient);

                //serializing the updated list back to the json file
                jsonData = JsonConvert.SerializeObject(patientsList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);

                //inform the user that the registration was successful
                await DisplayAlert("Success", "The new patient has been registered.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                await DisplayAlert("Error", "An error occurred while processing the registration.", "OK");
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Error loading the files or data.", "OK");
        }
    }
}
