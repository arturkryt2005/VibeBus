using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using VipeBus.Application.Entities.Cities;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewCity : Window
    {
        private readonly CityWindow _city;

        private VipeBusContext _context;

        public NewCity(CityWindow city)
        {
            InitializeComponent();

            _city = city;
            _context = new VipeBusContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckingConditions()) 
                return;

            var newCity = new City
            {
                Name = cityTextBox.Text,
                Region = regionTextBox.Text
            };

            _context.Cities.Add(newCity);
            _context.SaveChanges();

            _city.cityDataGrid.ItemsSource = _context.Cities
                .ToList();

            Close();
        }

        private bool CheckingConditions()
        {
            if (string.IsNullOrWhiteSpace(cityTextBox.Text))
            {
                MessageBox.Show("Введите название города.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (!Regex.IsMatch(cityTextBox.Text, "^[a-zA-Z0-9 ]+$"))
            {
                MessageBox.Show("Недопустимые символы в поле названия города.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (string.IsNullOrWhiteSpace(regionTextBox.Text))
            {
                MessageBox.Show("Выберите регион города.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            if (_context.Cities.Any(c => c.Name == cityTextBox.Text && c.Region == regionTextBox.Text))
            {
                MessageBox.Show("Такой город-регион уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

