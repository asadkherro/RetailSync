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
            if (IsFormValid())
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;
                bool rememberMe = RememberMeCheckBox.IsChecked == true;

                // Here you would typically:
                // 1. Send credentials to your API/authentication service
                // 2. Handle success/error responses
                // 3. Store authentication tokens if needed
                // 4. Navigate to main application window

                MessageBox.Show($"Login attempted for: {email}\nRemember me: {rememberMe}",
                               "Login", MessageBoxButton.OK, MessageBoxImage.Information);

                // For demo purposes, just show success message
                // In a real app, you'd authenticate and navigate to main window
                // Example: 
                // var mainWindow = new MainWindow();
                // mainWindow.Show();
                // this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid email and password.",
                               "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GoogleSignInButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Google Sign In
            // This would typically integrate with Google OAuth
            MessageBox.Show("Google Sign In would be implemented here.",
                           "Google Sign In", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example implementation would involve:
            // 1. Initialize Google OAuth client
            // 2. Redirect to Google sign in
            // 3. Handle callback with authorization code
            // 4. Exchange code for access token
            // 5. Get user profile information
            // 6. Create/login user in your system
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            // Handle forgot password
            // This could open a new window or dialog for password reset
            MessageBox.Show("Forgot password functionality would be implemented here.",
                           "Forgot Password", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example: 
            // var forgotPasswordWindow = new ForgotPasswordWindow();
            // forgotPasswordWindow.ShowDialog();
        }

        private void CreateAccount_Click(object sender, MouseButtonEventArgs e)
        {
            // Navigate to registration window
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
