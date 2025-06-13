using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RetailSync.ViewModels.Auth;

namespace RetailSync.Views.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AddValidationEvents();
            DataContext = new LoginViewModel();
        }
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel && sender is PasswordBox pbox)
            {
                viewModel.Password = pbox.Password;
            }
        }
        private void AddValidationEvents()
        {
            EmailTextBox.TextChanged += ValidateForm;
            PasswordBox.PasswordChanged += ValidateForm;
        }

        private void ValidateForm(object sender, EventArgs e)
        {
            bool isValid = IsFormValid();
            LoginButton.IsEnabled = isValid;
            LoginButton.Opacity = isValid ? 1.0 : 0.6;
        }

        private bool IsFormValid()
        {
            return IsValidEmail(EmailTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(PasswordBox.Password);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GoogleSignInButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Google Sign In would be implemented here.",
                           "Google Sign In", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
        }

        private void CreateAccount_Click(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        // Handle Enter key for login
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter && LoginButton.IsEnabled)
            {
                LoginButton_Click(LoginButton, new RoutedEventArgs());
            }
            base.OnKeyDown(e);
        }

        // Optional: Handle Tab navigation
        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                PasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && LoginButton.IsEnabled)
            {
                LoginButton_Click(LoginButton, new RoutedEventArgs());
            }
        }

        // Method to set image sources from code-behind if needed
        public void SetImageSources(string googleIconPath, string illustrationPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(googleIconPath))
                {
                    GoogleIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(googleIconPath, UriKind.RelativeOrAbsolute));
                }

                if (!string.IsNullOrEmpty(illustrationPath))
                {
                    MainIllustration.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(illustrationPath, UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                // Handle image loading errors
                System.Diagnostics.Debug.WriteLine($"Error loading images: {ex.Message}");
            }
        }
    }
}
