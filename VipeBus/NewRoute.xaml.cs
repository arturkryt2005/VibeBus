using System;
using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Cities;
using VipeBus.Application.Entities.Drivers;
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
                    DeparturePointId = ((City)FromComboBox.SelectedItem).Id,
                    DestinationPointId = ((City)toComboBox.SelectedItem).Id,
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
                    DriverId = ((Driver)DriverComboBox.SelectedItem).Id
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

                    MessageBox.Show($"Маршрут {((City)FromComboBox.SelectedItem).Name} - {((City)toComboBox.SelectedItem).Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

            Close();
        }

        private bool CheckingConditions()
        {
            var errorMessage = string.Empty;
            
            if (FromComboBox.SelectedItem == null)
                errorMessage += "Заполните место отпраления.\n";

            if (toComboBox.SelectedItem == null)
                errorMessage += "Заполните место назначения.\n";

            if (departureTimePicker.Value == null)
                errorMessage += "Заполните время отъезда.\n";

            if (arrivalTimePicker.Value == null)
                errorMessage += "Заполните время приезда.\n";

            if (departureDatePicker.SelectedDate == null)
                errorMessage += "Заполните дату отправления.\n";

            if (arrivalDatePicker.SelectedDate == null)
                errorMessage += "Заполните дату приезда.\n";

            if (departureDatePicker.SelectedDate >= arrivalDatePicker.SelectedDate)
                errorMessage += "Время отправления должно быть раньше времени прибытия.";

            if (((City)FromComboBox.SelectedItem).Id == ((City)FromComboBox.SelectedItem).Id)
                errorMessage += "Город отправления и город назначения должны быть разными.";

            if (errorMessage == string.Empty)
                return false;

            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

