using System.Diagnostics;
using System.Linq;

namespace hospitalSpace
{
    public partial class MainPage : ContentPage
    {
        private PatientLog logg = new PatientLog();
        private List<Staff> theStaffs;
        
        public MainPage()
        {
            InitializeComponent();


            Task.Run(async () =>
            {
                ReadJsonStaff jsonReader = new ReadJsonStaff();
                string filePath = "Contents/Resources/generated_staff.json";
                theStaffs = jsonReader.ReadStaffFromJson(filePath);

                if (theStaffs == null)
                {
                    await DisplayAlert("Error", "Error loading the files or data.", "OK");
                }
                else
                {
                   
                    HashSet<string> uniqueAreas = new HashSet<string>();
                    foreach (var person in theStaffs)
                    {
                        string area = person.Specialty;
                        uniqueAreas.Add(area);
                    }

                    List<string> areaList = uniqueAreas.ToList();


                    await StaffInfoList.Dispatcher.DispatchAsync(() =>
                    {
                        StaffInfoList.ItemsSource = theStaffs;
                        
                    });

                    await AreaListView.Dispatcher.DispatchAsync(() =>
                    {
                        AreaListView.ItemsSource = areaList;

                    });
                }
            });

        }

        private void OnPatientsButtonClicked(object sender, EventArgs e)
        {
            //make the search bar visible after clicking the patients button

            searchFrame.IsVisible = !searchFrame.IsVisible;
        }

        private async void SearchBoxEnterMethod(object sender, EventArgs e)
        {
            await AttemptSearch();
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            await AttemptSearch();
        }

        private async Task AttemptSearch()
        {
            int Healthcarenumber;
            string HCN = searchBar.Text;

            if (int.TryParse(searchBar.Text, out Healthcarenumber))
            {
                if (searchBar.Text.Length != 6)
                {
                    await DisplayAlert("Error", "Healthcare Number should have 6 digits.", "OK");
                    return;
                }else{

                    RandomPeople selectedPatient = null;

                    //lambda function to match the user input in the search bar with the info from the .json file

                    bool result = await Task.Run(() => logg.PatientInfo(Healthcarenumber, out selectedPatient));

                    if (selectedPatient != null)
                    {
                        await DisplayAlert("Success", "Patient Found.", "OK");
                        //open new page after finding patient
                        await Navigation.PushAsync(new PatientFound(selectedPatient));
                    }
                    else
                    {
                        bool answer = await DisplayAlert("Error", "Healthcare Number not found.\n Do you wish to create a new patient profile?", "No", "Yes");
                        if (!answer)
                        {
                            await Navigation.PushAsync(new NewPatientsPage(HCN));
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid healthcare number format.", "OK");
            }
        }

        private async void SearchBoxEnterMethodStaff(object sender, EventArgs e)
        {
            await AttemptSearchStaff();
        }

        private async void OnSearchStaffButtonPressed(object sender, EventArgs e)
        {
            await AttemptSearchStaff();
        }

        private async Task AttemptSearchStaff()
        {

            int StaffNum;

            if (int.TryParse(searchStaffFromNum.Text, out StaffNum))
            {
                if (searchStaffFromNum.Text.Length != 6)
                {
                    await DisplayAlert("Error", "Staff Number should have 6 digits.", "OK");
                    return;
                }
                else if (!int.TryParse(searchStaffFromNum.Text, out StaffNum))
                {
                    await DisplayAlert("Error", "Invalid Staff Number format.", "OK");
                    return;

                }
                else
                {
                    Staff selectedStaff = null;

                    foreach (var staff in theStaffs)
                    {
                        if (staff.StaffNumber == StaffNum)
                        {
                            selectedStaff = staff;
                            break;
                        }
                    }

                    if (selectedStaff != null)
                    {
                        await DisplayAlert("Success", "Staff Found.", "OK");
                        await Navigation.PushAsync(new StaffFound(selectedStaff));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Staff Number not found.", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Staff Number format.", "OK");
            }

        }

        void TheList_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                StaffInfoList.ItemsSource = theStaffs.Where(item => item.Specialty.Contains(e.SelectedItem as String));
                DisplayNursesFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
                DisplayDoctorsFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
                DisplayNurses.BackgroundColor = Color.FromArgb("#f2f2f2");
                DisplayDoctors.BackgroundColor = Color.FromArgb("#f2f2f2");
            }
        }


        void DisplayNurses_Clicked(System.Object sender, System.EventArgs e)
        {
            StaffInfoList.ItemsSource = theStaffs.Where(item => item.Occupation.Contains("Nurse"));
            DisplayNursesFrame.BackgroundColor = Color.FromArgb("#dcdcdc");
            DisplayDoctorsFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayNurses.BackgroundColor = Color.FromArgb("#dcdcdc");
            DisplayDoctors.BackgroundColor = Color.FromArgb("#f2f2f2");
            AreaListView.SelectedItem = null;
        }


        void DisplayDoctors_Clicked(System.Object sender, System.EventArgs e)
        {
            StaffInfoList.ItemsSource = theStaffs.Where(item => item.Occupation.Contains("Doctor"));
            DisplayNursesFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayDoctorsFrame.BackgroundColor = Color.FromArgb("#dcdcdc");
            DisplayNurses.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayDoctors.BackgroundColor = Color.FromArgb("#dcdcdc");
            AreaListView.SelectedItem = null;
        }

        void RemoveFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            StaffInfoList.ItemsSource = theStaffs;
            DisplayNursesFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayDoctorsFrame.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayNurses.BackgroundColor = Color.FromArgb("#f2f2f2");
            DisplayDoctors.BackgroundColor = Color.FromArgb("#f2f2f2");
            AreaListView.SelectedItem = null;
            StaffInfoList.SelectedItem = null;
        }
    }
}