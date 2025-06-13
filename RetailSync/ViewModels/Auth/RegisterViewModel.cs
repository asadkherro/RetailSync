using System.Windows;
using RetailSync.Core;
using RetailSync.Socket;
using RetailSync.Views.Auth;
using RetailSync_Models.DbModels;

namespace RetailSync.ViewModels.Auth
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly SocketClient _socketClient = new SocketClient();
        public RegisterViewModel()
        {

        }

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
            set { _email = value; OnPropertyChange(nameof(Email)); }
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

        public RelayCommand SignUpCommand => new RelayCommand(async _ =>
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
                PhoneNumber = PhoneNumber,
                Role = RetailSync_Models.UserRole.Employee
            };
            var result = await _socketClient.AddUserAsync(user);
            if (result.Success)
            {
                MessageBox.Show("Account Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                WindowManager.ChangeWindow<LoginWindow>(this, new LoginViewModel());
            }
            else
                MessageBox.Show("An error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            ResetForm();
        });

        private void ResetForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            
            OnPropertyChange(nameof(FirstName));
            OnPropertyChange(nameof(LastName));
            OnPropertyChange(nameof(Email));
            OnPropertyChange(nameof(Password));
            OnPropertyChange(nameof(PhoneNumber));
        }
    }
}
