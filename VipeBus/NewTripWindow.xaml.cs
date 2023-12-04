using System;
using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Application.Entities.Routes;
using VipeBus.Application.Entities.Trips;
using VipeBus.Application.Entities.Users;
using VipeBus.Core;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для NewTripWindow.xaml
    /// </summary>
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
            if (string.IsNullOrWhiteSpace(TripNameTextBox.Text))
            {
                MessageBox.Show("Введите наименование поездки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrWhiteSpace(BusComboBox.Text))
            {
                MessageBox.Show("Выберите номер автобуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrWhiteSpace(UserComboBox.Text))
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrWhiteSpace(RouteComboBox.Text))
            {
                MessageBox.Show("Выберите маршрут.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            return false;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
