using System.Linq;
using System.Diagnostics;

namespace hospitalSpace;

public partial class MedicalHist : ContentPage
{
    public RandomPeople SelectedPatient { get; set; }

    public MedicalHist()
    {
        InitializeComponent();
    }

    //don't forget the constructor that accepts random people object so u can access the patient choosen
    public MedicalHist(RandomPeople patient) : this()
    {
        try
        {
            //if the list is blank it tell u so, otherwise it shows the exams attributed
            SelectedPatient = patient;

            if (SelectedPatient.ExamRecords == null || !SelectedPatient.ExamRecords.Any() && !SelectedPatient.examsScheduled.Any())
            {
                ExamsLabel.Text = "No exams logged yet.";
            }
            else
            {
                if (SelectedPatient.ExamRecords.Any())
                {
                    ExamsLabel.Text = string.Join("\n", SelectedPatient.ExamRecords) + "\n" + string.Join("\n", SelectedPatient.examsScheduled.Select(m => $"{m.area} - {m.doctor_assigned} - {m.date_picked}"));
                }
                else
                {
                    ExamsLabel.Text = string.Join("\n", SelectedPatient.examsScheduled.Select(m => $"{m.area} - {m.doctor_assigned} - {m.date_picked}"));
                }
            }

            if (SelectedPatient.Medicineassignedtothispatient == null || !SelectedPatient.Medicineassignedtothispatient.Any())
            {
                MedicineLabel.Text = "No medicine attributed yet.";
            }
            else
            {
                MedicineLabel.Text = string.Join("\n", SelectedPatient.Medicineassignedtothispatient.Select(m => $"{m.name}: {m.price}€"));
            }

            if (SelectedPatient.appointmentsScheduled == null || !SelectedPatient.appointmentsScheduled.Any())
            {
                AppointmentsLabel.Text = "No appointments logged yet.";
            }
            else
            {
                AppointmentsLabel.Text = string.Join("\n", SelectedPatient.appointmentsScheduled.Select(m => $"{m.area} - {m.doctor_assigned} - {m.date_picked}"));
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

}
