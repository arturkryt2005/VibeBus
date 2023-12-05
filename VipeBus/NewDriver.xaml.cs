using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewDriver : Window
    {
        private VipeBusContext _context;

        private readonly DriverWindow _driver;

        public NewDriver(DriverWindow driver)
        {
            InitializeComponent();

            _driver = driver;

            using (_context = new VipeBusContext())
            {
                var result = _context.Buses
                    .ToList();

                BusComboBox.ItemsSource = result;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Введите фамилию водителя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Введите имя водителя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MiddleNameTextBox.Text))
            {
                MessageBox.Show("Введите отчество водителя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




            if (string.IsNullOrWhiteSpace(BusComboBox.Text))
            {
                MessageBox.Show("Выберите номер автобуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (_context = new VipeBusContext())
            {
                var newDriver = new Application.Entities.Drivers.Driver
                {
                    Name = $"{LastNameTextBox.Text} {FirstNameTextBox.Text} {MiddleNameTextBox.Text}",
                    BusId = ((Bus)BusComboBox.SelectedItem).Id,
                };

                try
                {
                    _context.Drivers.Add(newDriver);
                    _context.SaveChanges();

                    _driver.driverDataGrid.ItemsSource = _context.Drivers
                        .Include("Bus")
                        .ToList();

                    _context.Dispose();

                    MessageBox.Show($"Водитель {newDriver.Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

            Close();
        }
        private bool IsValidInput(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9 ]+$");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
