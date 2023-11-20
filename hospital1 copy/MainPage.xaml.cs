using authentication_program;
using hospital1;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Diagnostics;

namespace hospital1;

public partial class MainPage : ContentPage
{
    private Authentication auth = new Authentication();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnEntryPasswordCompleted(object sender, EventArgs e)
    {
        await AttemptLogin();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await AttemptLogin();
    }

    private async Task AttemptLogin()
    {

        string username = (EntryUsername.Text ?? string.Empty).Trim();
        int password;

        if (int.TryParse(EntryPassword.Text, out password))
        {

            bool result = auth.Authentication_(username, password);

            if (result)
            {
                await DisplayAlert("Success", "Login successful.", "OK");
                await Navigation.PushAsync(new NewPage1());
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Invalid password format.", "OK");
        }
    }
}
