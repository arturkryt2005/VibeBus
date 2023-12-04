using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class HeadWindow : Window
    {
        private VipeBusContext _context;

        public HeadWindow()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            tripDataGrid.ItemsSource = _context.Trips
                .Include("User")
                .Include("Bus")
                .Include("Route")
                .ToList();
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            var routeWindow = new NewTripWindow(this)
            {
                Title = "Маршруты"
            };
            routeWindow.Show();
        }

        private void RouteButton_Click(object sender, RoutedEventArgs e)
        {
            var routeWindow = new RouteWindow
            {
                Title = "Маршруты"
            };
            routeWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var driverWindow = new Driver
            {
                Title = "Водители"
            };
            driverWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var city = new CityWindow();
            city.Show();
            this.Close();
        }
    }
}
