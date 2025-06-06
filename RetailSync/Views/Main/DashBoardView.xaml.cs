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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using RetailSync.ViewModels.Main;

namespace RetailSync.Views.Main
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        public DashBoardView()
        {
            InitializeComponent();
            InitializeCharts();
            DataContext = new DashboardViewModel();
        }
        private void InitializeCharts()
        {
            InitializeBarChart();
            InitializePieChart();
        }

        private void InitializeBarChart()
        {
            // Create series for the bar chart
            var orderSeries = new ColumnSeries
            {
                Title = "Order",
                Values = new ChartValues<double> { 80, 60, 70, 65, 75 },
                Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15)), // #F1C40F
                MaxColumnWidth = 40,
                ColumnPadding = 2
            };

            var salesSeries = new ColumnSeries
            {
                Title = "Sales",
                Values = new ChartValues<double> { 60, 90, 100, 95, 105 },
                Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219)), // #3498DB
                MaxColumnWidth = 40,
                ColumnPadding = 2
            };

            // Configure the bar chart
            BarChart.Series = new SeriesCollection { orderSeries, salesSeries };

            // Configure X axis labels (days of week or time periods)
            BarChart.AxisX.Add(new Axis
            {
                Labels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri" },
                ShowLabels = true,
                Separator = new LiveCharts.Wpf.Separator
                {
                    IsEnabled = false
                }
            });

            // Configure Y axis
            BarChart.AxisY.Add(new Axis
            {
                ShowLabels = false,
                Separator = new LiveCharts.Wpf.Separator
                {
                    IsEnabled = false
                }
            });

            // Remove legends as we have custom legend
            BarChart.LegendLocation = LegendLocation.None;
        }

        private void InitializePieChart()
        {
            // Create pie chart data
            var pieData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Product A",
                    Values = new ChartValues<double> { 45 },
                    Fill = new SolidColorBrush(Color.FromRgb(44, 62, 80)), // #2C3E50 - Dark blue
                    DataLabels = false
                },
                new PieSeries
                {
                    Title = "Product B",
                    Values = new ChartValues<double> { 25 },
                    Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219)), // #3498DB - Blue
                    DataLabels = false
                },
                new PieSeries
                {
                    Title = "Product C",
                    Values = new ChartValues<double> { 20 },
                    Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15)), // #F1C40F - Yellow
                    DataLabels = false
                },
                new PieSeries
                {
                    Title = "Product D",
                    Values = new ChartValues<double> { 10 },
                    Fill = new SolidColorBrush(Color.FromRgb(231, 76, 60)), // #E74C3C - Red
                    DataLabels = false
                }
            };

            PieChart.Series = pieData;
            PieChart.LegendLocation = LegendLocation.None;
        }

        // Method to update statistics (can be called from external data source)
        public void UpdateStatistics(int sales, int orders, int revenue, int customers)
        {
            // You can update the TextBlocks here if you want dynamic data
            // This would typically be called when new data arrives
        }

        // Method to update chart data
        public void UpdateChartData(double[] orderData, double[] salesData)
        {
            if (BarChart.Series.Count >= 2)
            {
                ((ColumnSeries)BarChart.Series[0]).Values.Clear();
                ((ColumnSeries)BarChart.Series[1]).Values.Clear();

                foreach (var value in orderData)
                {
                    ((ColumnSeries)BarChart.Series[0]).Values.Add(value);
                }

                foreach (var value in salesData)
                {
                    ((ColumnSeries)BarChart.Series[1]).Values.Add(value);
                }
            }
        }

        // Handle menu navigation (add click events for menu buttons)
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            // Already on dashboard
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to products page
            MessageBox.Show("Navigate to Products page", "Navigation", MessageBoxButton.OK);
        }

        private void ProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to projects page
            MessageBox.Show("Navigate to Projects page", "Navigation", MessageBoxButton.OK);
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to sales page
            MessageBox.Show("Navigate to Sales page", "Navigation", MessageBoxButton.OK);
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to users page
            MessageBox.Show("Navigate to Users page", "Navigation", MessageBoxButton.OK);
        }

        // Handle profile dropdown
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Show profile menu or navigate to profile
            MessageBox.Show("Profile menu", "Profile", MessageBoxButton.OK);
        }

        // Simulate real-time data updates
        private void StartDataSimulation()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += (s, e) =>
            {
                // Simulate updating chart data
                var random = new Random();
                var orderData = new double[5];
                var salesData = new double[5];

                for (int i = 0; i < 5; i++)
                {
                    orderData[i] = random.Next(50, 100);
                    salesData[i] = random.Next(60, 120);
                }

                UpdateChartData(orderData, salesData);
            };
            // Uncomment to enable real-time updates
            // timer.Start();
        }
    }
}
