using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class CityWindow : Window
    {
        private VipeBusContext _context;

        public CityWindow()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            cityDataGrid.ItemsSource = _context.Cities
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newCity = new NewCity(this)
            {
                Title = "Добавить город"
            };
            newCity.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (cityDataGrid.SelectedItem != null)
            {
                var selectedCity = (Application.Entities.Cities.City)cityDataGrid.SelectedItem;

                if (!_context.Cities.Local.Contains(selectedCity))
                    _context.Cities.Attach(selectedCity);

                if (_context.Routes.Any(r =>
                        r.DeparturePointId == selectedCity.Id || r.DestinationPoint.Id == selectedCity.Id))
                {
                    MessageBox.Show($"Нельзя удалить город, так как он добавлен в маршрут.");
                    return;
                }

                if (MessageBox.Show($"Вы уверены, что хотите удалить город?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Cities.Remove(selectedCity);
                    _context.SaveChanges();

                    cityDataGrid.ItemsSource = _context.Cities
                        .ToList();
                }
            }
            else
                MessageBox.Show("Выберите город для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Главная"
            };
            headWindow.Show();
            this.Close();
        }
    }
}

