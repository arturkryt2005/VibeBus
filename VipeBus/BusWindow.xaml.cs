using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Core;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        private VipeBusContext _context;

        public BusWindow()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            BusDataGrid.ItemsSource = _context.Buses
                .ToList();
        }

        private void AddBusButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newBusWindow = new NewBusWindow(this)
            {
                Title = "Добавить автобус"
            };

            newBusWindow.Show();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Главная"
            };

            headWindow.Show();
            this.Close();
        }

        private void DeleteBusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (BusDataGrid.SelectedItem != null)
            {
                var selectedBus = (Bus)BusDataGrid.SelectedItem;

                if (_context.Drivers.Any(driver => driver.BusId == selectedBus.Id))
                {
                    MessageBox.Show("Нельзя удалить автобус, который занят водителем.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!_context.Buses.Local.Contains(selectedBus))
                    _context.Buses.Attach(selectedBus);

                if (MessageBox.Show($"Вы уверены, что хотите удалить автобус?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Buses.Remove(selectedBus);
                    _context.SaveChanges();

                    BusDataGrid.ItemsSource = _context.Buses
                        .ToList();
                }
            }
            else
                MessageBox.Show("Выберите автобус для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
