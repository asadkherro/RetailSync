using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Hyperlink_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Terms and conditions dialog would open here.",
                           "Terms", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SignUpButton.IsEnabled)
            {
                SignUpButton_Click(SignUpButton, new RoutedEventArgs());
            }
            base.OnKeyDown(e);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel model && sender is PasswordBox pBox)
            {
                model.Password = pBox.Password;
            }
        }
    }
}
