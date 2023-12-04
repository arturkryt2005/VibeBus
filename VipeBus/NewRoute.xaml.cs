using System;
using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewRoute : Window
    {
        private VipeBusContext _context;

        private readonly RouteWindow _route;

        public NewRoute(RouteWindow route)
        {
            InitializeComponent();

            _context = new VipeBusContext();
            _route = route;

            FillComboBox();

        }

        private void FillComboBox()
        {
            FromComboBox.ItemsSource = _context.Cities.ToList();
            toComboBox.ItemsSource = _context.Cities.ToList();
            DriverComboBox.ItemsSource = _context.Drivers.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckingConditions()) 
                return;

            using (_context = new VipeBusContext())
            {
                var newRoute = new Application.Entities.Routes.Route()
                {
                    DeparturePointId = ((Application.Entities.Cities.City)FromComboBox.SelectedItem).Id,
                    DestinationPointId = ((Application.Entities.Cities.City)toComboBox.SelectedItem).Id,
                    DepartureTime = new DateTime(departureDatePicker.SelectedDate.Value.Year,
                        departureDatePicker.SelectedDate.Value.Month,
                        departureDatePicker.SelectedDate.Value.Day,
                        departureTimePicker.Value.Value.Hour,
                        departureTimePicker.Value.Value.Minute,
                        departureTimePicker.Value.Value.Second),
                    DestinationTime = new DateTime(arrivalDatePicker.SelectedDate.Value.Year,
                        arrivalDatePicker.SelectedDate.Value.Month,
                        arrivalDatePicker.SelectedDate.Value.Day,
                        arrivalTimePicker.Value.Value.Hour,
                        arrivalTimePicker.Value.Value.Minute,
                        arrivalTimePicker.Value.Value.Second),
                    DriverId = ((Application.Entities.Drivers.Driver)DriverComboBox.SelectedItem).Id
                };

                try
                {
                    _context.Routes.Add(newRoute);
                    _context.SaveChanges();

                    _route.routeDataGrid.ItemsSource = _context.Routes
                        .Include("DeparturePoint")
                        .Include("DestinationPoint")
                        .Include("Driver")
                        .ToList();

                    MessageBox.Show(
                        $"Маршрут {((Application.Entities.Cities.City)FromComboBox.SelectedItem).Name} - {((Application.Entities.Cities.City)toComboBox.SelectedItem).Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            Close();
        }

        private bool CheckingConditions()
        {
            var errorMessage = string.Empty;

            if (FromComboBox.SelectedItem == null)
                errorMessage += "Заполните место отпраления";

            if (toComboBox.SelectedItem == null)
                errorMessage += "Заполните место назначения.";

            if (departureTimePicker.Value == null)
                errorMessage += "Заполните время отъезда.";

            if (arrivalTimePicker.Value == null)
                errorMessage += "Заполните время приезда.";

            if (departureDatePicker.SelectedDate == null)
                errorMessage += "Заполните дату отправления.";

            if (arrivalDatePicker.SelectedDate == null)
                errorMessage += "Заполните дату приезда.";

            if(errorMessage == string.Empty)
                return false;

            else
            {
                MessageBox.Show(errorMessage);
                return true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

