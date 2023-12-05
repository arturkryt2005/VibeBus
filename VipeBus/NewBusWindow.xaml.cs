using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Core;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для NewBusWindow.xaml
    /// </summary>
    public partial class NewBusWindow : Window
    {
        private VipeBusContext _context;

        private readonly BusWindow _busWindow;

        public NewBusWindow(BusWindow busWindow)
        {
            InitializeComponent();

            _busWindow = busWindow;
            _context = new VipeBusContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckingConditions())
                return;

            var newBus = new Bus
            {
                Name = NameTextBox.Text,
                Number = Convert.ToInt32(NumberTextBox.Text)
            };

            _context.Buses.Add(newBus);
            _context.SaveChanges();

            _busWindow.BusDataGrid.ItemsSource = _context.Buses
                .ToList();

            Close();
        }



            private bool CheckingConditions()
            {
                if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                {
                    MessageBox.Show("Введите наименование автобуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }

                if (!Regex.IsMatch(NameTextBox.Text, "^[a-zA-Z0-9 ]+$"))
                {
                    MessageBox.Show("Недопустимые символы в поле наименования автобуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }

                if (int.TryParse(NumberTextBox.Text, out var result))
                {
                    if (string.IsNullOrWhiteSpace(NumberTextBox.Text))
                    {
                        MessageBox.Show("Введите номер автобуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("В номере автобуса допустимы только числовые значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }

                if (_context.Buses.Any(c => c.Name == NameTextBox.Text && c.Number.ToString() == NumberTextBox.Text))
                {
                    MessageBox.Show("Такой автобус уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }

                return false;
            }

            private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
