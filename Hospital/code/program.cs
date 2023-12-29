

using System.ComponentModel;

namespace hospitalSpace
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
            new Login("admin4", 000002, "Olivia", "Alexandra"),
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

    public class Item : INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}