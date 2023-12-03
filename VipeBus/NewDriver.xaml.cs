using System;
using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewDriver : Window
    {
        private VipeBusContext _context;

        private Driver _driver;

        public NewDriver(Driver driver)
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
            if (FirstNameTextBox.Text == string.Empty
                || LastNameTextBox.Text == string.Empty
                || MiddleNameTextBox.Text == string.Empty
                || BusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не заполнены все поля.");
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

                    _driver.tripDataGrid.ItemsSource = _context.Drivers
                        .Include("Bus")
                        .ToList();

                    _context.Dispose();

                    MessageBox.Show($"Водитель {newDriver.Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }

            Close();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно
            this.Close();
        }
    }
}
