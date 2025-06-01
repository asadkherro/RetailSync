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
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // Handle hyperlink clicks (terms, privacy policy, etc.)
        private void Hyperlink_Click(object sender, MouseButtonEventArgs e)
        {
            // Handle clicking on terms, privacy policy links
            // You could open web browser or show dialog with terms
            MessageBox.Show("Terms and conditions dialog would open here.",
                           "Terms", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Optional: Add enter key handling
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SignUpButton.IsEnabled)
            {
                SignUpButton_Click(SignUpButton, new RoutedEventArgs());
            }
            base.OnKeyDown(e);
        }
    }
}
