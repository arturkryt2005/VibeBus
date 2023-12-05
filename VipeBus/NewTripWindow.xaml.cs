using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Application.Entities.Routes;
using VipeBus.Application.Entities.Trips;
using VipeBus.Application.Entities.Users;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewTripWindow : Window
    {
        private VipeBusContext _context;
        private readonly HeadWindow _headWindow;

        public NewTripWindow(HeadWindow headWindow)
        {
            InitializeComponent();

            _headWindow = headWindow;
            _context = new VipeBusContext();

            FillComboBox();
        }

        private void FillComboBox()
        {
            BusComboBox.ItemsSource = _context.Buses
                .ToList();
            UserComboBox.ItemsSource = _context.Users
                .ToList();
            RouteComboBox.ItemsSource = _context.Routes
                .ToList();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckingConditions())
                return;

            var newTrip = new Trip
            {
                Name = TripNameTextBox.Text,
                BusId = ((Bus)BusComboBox.SelectedItem).Id,
                RouteId = ((Route)RouteComboBox.SelectedItem).Id,
                UserId = ((User)UserComboBox.SelectedItem).Id
            };

            try
            {
                _context.Trips.Add(newTrip);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            _headWindow.tripDataGrid.ItemsSource = _context.Trips
                    .Include("User")
                    .Include("Bus")
                    .Include("Route")
                    .ToList();

            MessageBox.Show($"Поездка {newTrip.Name} успешно добавлена.");

            Close();
        }

        private bool CheckingConditions()
        {
            var errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(TripNameTextBox.Text))
                errorMessage += "Введите наименование поездки.\n";

            if (string.IsNullOrWhiteSpace(BusComboBox.Text))
                errorMessage += "Выберите номер автобуса.\n";

            if (string.IsNullOrWhiteSpace(UserComboBox.Text))
                errorMessage += "Выберите клиента.\n";

            if (string.IsNullOrWhiteSpace(RouteComboBox.Text))
                errorMessage += "Выберите маршрут.\n";

            if (!IsValidInput(TripNameTextBox.Text))
                errorMessage += "Используйте только буквы, цифры и пробелы в наименовании поездки.\n";

            if (errorMessage == string.Empty)
                return false;

            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }

        private bool IsValidInput(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9 ]+$");
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}