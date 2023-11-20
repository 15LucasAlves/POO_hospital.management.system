namespace hospital1;
using betahospital;
using authentication_program;

public partial class NewPage2 : ContentPage
{
	public NewPage2()
	{
		InitializeComponent();
	}
    public NewPage2(RandomPeople patient)
    {
        SelectedPatient = patient;

    }

    public RandomPeople SelectedPatient { get; set; }
}