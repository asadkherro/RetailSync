using System.Windows;
using RetailSync.Core;
using RetailSync.Socket;
using RetailSync_Models.DbModels;

namespace RetailSync.ViewModels.Auth
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly SocketClient _socketClient = new SocketClient();
        public RegisterViewModel()
        {

        }

        #region Properties
        
        /// <summary>
        /// Properties of RegisterViewModel
        /// </summary>
        
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChange(nameof(FirstName)); }
        }

        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; OnPropertyChange(nameof(LastName)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChange(nameof(Email));}
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChange(nameof(Password)); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChange(nameof(PhoneNumber)); }
        }

        #endregion Properties

        #region Commands

        public RelayCommand SignUpCommand => new RelayCommand(async _ =>
        {
            await CreateAccount();
        });

        #endregion Commands

        #region Methods

        private async Task CreateAccount()
        {

            bool connected = await _socketClient.ConnectAsync("localhost", 5000);
            if (!connected)
                return;

            UserModel user = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = $"{FirstName} {LastName}",
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber
            };
            await _socketClient.AddUserAsync(user);

            MessageBox.Show("Account Registered");
        }

        #endregion Methods
    }
}
