using System.Windows;
using RetailSync.Core;
using RetailSync.Socket;
using RetailSync_Models.DbModels;

namespace RetailSync.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly SocketClient _socketClient = new SocketClient();

        public LoginViewModel() { }

        #region Properties

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

        public bool ValidForm { get; set; }

        #endregion Properties

        #region Commands

        public RelayCommand LoginCommand => new RelayCommand(async _ => 
		{
            MessageBox.Show($"Login attempted for: {Email}\nPassword: {Password}",
               "Login Attempt", MessageBoxButton.OK, MessageBoxImage.Information);

            var user = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = Email,
                Password = Password
            };

            await _login(user);

        });

        #endregion Commands


        private async Task _login(UserModel user)
        {
            bool connected = await _socketClient.ConnectAsync("localhost", 5000);
            if (!connected)
                return;
            
            var addUser = await _socketClient.AddUserAsync(user);
        }
    }
}
