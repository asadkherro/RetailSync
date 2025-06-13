using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RetailSync.ViewModels.Main;

namespace RetailSync.Views.Main
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
            LoadSampleData();
            //DataContext = new ProductViewModel();
            DataContext = this;
        }
        public ObservableCollection<Product> Products { get; set; }

        private void LoadSampleData()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { ProductId = "#P001", Name = "Wireless Headphones", Category = "Electronics", Price = 99.99m, Stock = 150, Status = "Active", LastUpdated = DateTime.Parse("2024-06-01") },
                new Product { ProductId = "#P002", Name = "Gaming Mouse", Category = "Electronics", Price = 45.50m, Stock = 0, Status = "Out of Stock", LastUpdated = DateTime.Parse("2024-06-05") },
                new Product { ProductId = "#P003", Name = "Office Chair", Category = "Furniture", Price = 299.99m, Stock = 25, Status = "Active", LastUpdated = DateTime.Parse("2024-06-03") },
                new Product { ProductId = "#P004", Name = "Coffee Maker", Category = "Appliances", Price = 129.99m, Stock = 75, Status = "Active", LastUpdated = DateTime.Parse("2024-06-02") },
                new Product { ProductId = "#P005", Name = "Desk Lamp", Category = "Furniture", Price = 39.99m, Stock = 5, Status = "Low Stock", LastUpdated = DateTime.Parse("2024-06-04") }
            };
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export functionality would be implemented here.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Import functionality would be implemented here.", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewProductButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add new product functionality would be implemented here.", "New Product", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Search functionality would be implemented here
        }
    }
    public class Product : INotifyPropertyChanged
    {
        private string _productId;
        private string _name;
        private string _category;
        private decimal _price;
        private int _stock;
        private string _status;
        private DateTime _lastUpdated;

        public string ProductId
        {
            get => _productId;
            set { _productId = value; OnPropertyChanged(nameof(ProductId)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public int Stock
        {
            get => _stock;
            set { _stock = value; OnPropertyChanged(nameof(Stock)); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public DateTime LastUpdated
        {
            get => _lastUpdated;
            set { _lastUpdated = value; OnPropertyChanged(nameof(LastUpdated)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StatusToColorConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string status)
            {
                return status switch
                {
                    "Active" => new SolidColorBrush(Color.FromRgb(34, 197, 94)), // Green
                    "Out of Stock" => new SolidColorBrush(Color.FromRgb(239, 68, 68)), // Red
                    "Low Stock" => new SolidColorBrush(Color.FromRgb(245, 158, 11)), // Orange
                    _ => new SolidColorBrush(Colors.Gray)
                };
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
