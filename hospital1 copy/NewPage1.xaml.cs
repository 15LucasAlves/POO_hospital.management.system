using authentication_program;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using betahospital;

namespace hospital1
{
    public partial class NewPage1 : ContentPage
    {
        private PatientLog logg = new PatientLog();

        public NewPage1()
        {
            InitializeComponent();
        }
        private async void OnPatientsButtonClicked(object sender, EventArgs e)
        {
            //make the search bar visible after clicking the patients button

            searchFrame.IsVisible = !searchFrame.IsVisible;
        }
        private async void OnStaffButtonClicked(object sender, EventArgs e)
        {

        }
        //enter method for the search bar

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

            if (int.TryParse(searchBar.Text, out Healthcarenumber))
            {
                int count = 0;

                //counter for the healthcare number so you know if the number has the desired lenght

                int counthealthnumber = Healthcarenumber;
                while (counthealthnumber> 0)
                {
                    counthealthnumber = counthealthnumber / 10;
                    count++;
                }
                if (count != 6)
                {
                    await DisplayAlert("Error", "Healthcare Number should have 6 digits.", "OK");
                }
                else
                {
                    RandomPeople selectedPatient = null;

                    //lambda function to match the user input in the search bar with the info from the .json file

                    bool result = await Task.Run(() => logg.PatientInfo(Healthcarenumber, out selectedPatient));

                    if (selectedPatient != null)
                    {
                        await DisplayAlert("Success", "Patient Found.", "OK");
                        //open new page after finding patient
                        await Navigation.PushAsync(new NewPage2(selectedPatient));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Healthcare Number not found.", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid healthcare number format.", "OK");
            }
        }
    }
}