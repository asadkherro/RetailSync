using RetailSync.Core;

namespace RetailSync.ViewModels.Main
{
    public class DashboardViewModel : BaseViewModel
    {
        private string _name;

        public DashboardViewModel()
        {

        }

        public string Name
        {
            get => GetInitials(ApplicationCache.Instance.AppUser.Name);
            set
            {
                _name = value;
                OnPropertyChange(nameof(Name));
            }
        }
        private string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var array = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0)
                return string.Empty;

            return string.Concat(array.Select(word => word[0].ToString().ToUpper()));
        }
    }
}
