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

        public NewDriver()
        {
            InitializeComponent();

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

                    MessageBox.Show($"Водитель {newDriver.Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно
            this.Close();
        }
    }
}
