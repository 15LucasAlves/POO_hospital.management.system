namespace hospitalSpace;

public partial class StaffFound: ContentPage
{
    public Staff SelectedStaff { get; set; }

    public StaffFound()
    {
        InitializeComponent();
    }
    public StaffFound(Staff staff) : this()
    {
        SelectedStaff = staff;
        NameLabel.Text = "Name: " + SelectedStaff.Name;
        DateofBirthLabel.Text = "Date of Birth: " + SelectedStaff.DateofBirth;
        IdentityNum.Text = "ID: " + SelectedStaff.IdentityNumber.ToString();
        Ocupation.Text = "Occupation: " + SelectedStaff.Occupation;
        Specialty.Text = "Specialty: " + SelectedStaff.Specialty;
        StaffNum.Text = "Staff Number: " + SelectedStaff.StaffNumber.ToString();
        PhoneNum.Text = "Phone Number: " + SelectedStaff.PrivateNumber.ToString();
        ShiftStart.Text = "Shift Start: " + SelectedStaff.WorkingHours.Start.ToString();
        ShiftEnd.Text = "Shift End: " + SelectedStaff.WorkingHours.End.ToString();
    }
}
