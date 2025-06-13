using System.Windows;

namespace RetailSync.Core
{
    public static class WindowManager
    {
        public static void CloseWindow(object viewModel)
        {
            var window = Application.Current.Windows.Cast<Window>()
                .FirstOrDefault(w => w.DataContext == viewModel);
            window?.Close();
        }

        public static void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }

        public static void ShowWindow<T>(object viewModel) where T : Window, new()
        {
            var window = new T { DataContext = viewModel };
            window.Show();
        }

        public static void ChangeWindow<T>(object currentViewModel, object newViewModel) where T : Window, new()
        {
            ShowWindow<T>(newViewModel);
            CloseWindow(currentViewModel);
        }
    }
}
