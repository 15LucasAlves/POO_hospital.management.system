namespace hospital1;

public partial class popup : ContentPage
{
    public popup()
    {
        InitializeComponent();
    }
    private async void CollectionViewBloodTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedBloodType = e.CurrentSelection.FirstOrDefault() as string;
        if (selectedBloodType != null)
        {
            // Do something with the selected blood type, e.g., update a ViewModel property
            // Close the popup
            await Navigation.PopModalAsync();
        }
    }
}
