using System;
using System.Collections.Generic;
using authentication_program;
using betahospital;

namespace authentication_program
{
    class Login
    {
        public string username;
        public int password;
        public string name;
        public string lastname;

        public Login(string Username_, int Password_, string Name_, string Lastname_)
        {
            username = Username_;
            password = Password_;
            name = Name_;
            lastname = Lastname_;
        }
    }

    class Login_accounts
    {
        public Login[] mylogins;
        //creating an instance of Login and storing login data here


        public Login_accounts()
        {
            mylogins = new Login[] {
            // Create and initialize the mylogin object
            new Login("admin1", 123456, "Lara", "James"),
            new Login("admin2", 000000, "Ren", "Amamya"),
            new Login("admin3", 000001, "Ann", "Kim"),
            new Login("admin4", 123456, "Olivia", "Alexandra"),
            };
        }
    }

    class Authentication
    {
        Login_accounts loginAccounts = new Login_accounts();

        public bool Authentication_(string user, int pass)
        {

            // Check which Login object matches the user input
            foreach (Login mylogin in loginAccounts.mylogins)
            {

                if (mylogin.username == user && mylogin.password == pass)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class PatientLog
    {
        public bool PatientInfo(int healthcarenumber, out RandomPeople selectedPatient)
        {
            ReadJson jsonReader = new ReadJson();
            List<RandomPeople> patients = jsonReader.ReadPersonsFromJson("https://raw.githubusercontent.com/15LucasAlves/POO_hospital.management.system/main/random.json");

            selectedPatient = null;

            foreach (var patient in patients)
            {
                if (patient.Healthcarenumber == healthcarenumber)
                {
                    selectedPatient = patient;
                    break;
                }
            }

            if (selectedPatient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}