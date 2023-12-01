using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VipeBus
{
    public partial class HeadWindow : Window
    {
        public ObservableCollection<Trip> Trips { get; set; }

        public HeadWindow()
        {
            InitializeComponent();
            Trips = new ObservableCollection<Trip>();
            tripDataGrid.ItemsSource = Trips;
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            Trips.Add(new Trip());
        }

        private void RouteButton_Click(object sender, RoutedEventArgs e)
        {
            Route routeWindow = new Route();
            routeWindow.Title = "Маршруты";
            routeWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Driver driverWindow = new Driver();
            driverWindow.Title = "Водители";
            driverWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            City city = new City();
            city.Show();
            this.Close();
        }
    }

    public class Trip
    {
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DriverName { get; set; }
    }
}
