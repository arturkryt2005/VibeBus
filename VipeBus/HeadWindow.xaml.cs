using System.Data.Entity;
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

        private NewTripWindow routeWindow;

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            if (routeWindow == null || !routeWindow.IsVisible)
            {
                routeWindow = new NewTripWindow(this)
                {
                    Title = "Новая поездка"
                };
                routeWindow.Closed += (s, args) => routeWindow = null; 
                routeWindow.Show();
            }
            else
            {
                routeWindow.Activate(); 
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (tripDataGrid.SelectedItem != null)
            {
                var selectedTrip = (Application.Entities.Trips.Trip)tripDataGrid.SelectedItem;

                selectedTrip.Name = null;

                var existingEntity = _context.Trips.Find(selectedTrip.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(selectedTrip);

                    if (MessageBox.Show($"Вы уверены, что хотите удалить поездку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _context.Trips.Remove(existingEntity); 
                        _context.SaveChanges();

                        tripDataGrid.ItemsSource = _context.Trips.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите водителя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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

        private void Drivers_Click(object sender, RoutedEventArgs e)
        {
            var driverWindow = new DriverWindow
            {
                Title = "Водители"
            };
            driverWindow.Show();
            this.Close();
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            var city = new CityWindow();
            city.Show();
            this.Close();
        }

        private void BusButton_Click(object sender, RoutedEventArgs e)
        {
            var city = new BusWindow();
            city.Show();
            this.Close();
        }
    }
}