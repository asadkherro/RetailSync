using System.Text.Json;
using System.Windows;
using RetailSync.Core;
using RetailSync.Socket;
using RetailSync.ViewModels.Main;
using RetailSync.Views.Main;
using RetailSync_Models.DbModels;

namespace RetailSync.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly SocketClient _socketClient = new SocketClient();

        public LoginViewModel() { }

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

        public RelayCommand LoginCommand => new RelayCommand(async _ => 
		{
            bool connected = await _socketClient.ConnectAsync("localhost", 5000);
            if (!connected)
                return;

            var loginRes = await _socketClient.LoginUserAsync(Email, Password);
            if (loginRes != null && loginRes.Success && loginRes.Data != null)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                UserModel userModel = JsonSerializer.Deserialize<UserModel>(loginRes.Data);
                if (userModel == null)
                {
                    MessageBox.Show("An error occured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ApplicationCache cache = ApplicationCache.Instance;
                cache.SetAppUser(userModel);
                WindowManager.ChangeWindow<HomeWindow>(this, new HomeViewModel());
            }
            else
                MessageBox.Show($"Login failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            ResetForm();
        });

        private void ResetForm()
        {
            Email = string.Empty;
            Password = string.Empty;
            OnPropertyChange(nameof(Email));
            OnPropertyChange(nameof(Password));
        }
    }
}
