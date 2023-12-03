using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewCity : Window
    {
        private City City;

        private VipeBusContext _context;

        public NewCity(City city)
        {
            InitializeComponent();

            City = city;
            _context = new VipeBusContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Entities.Cities.City newCity = new Application.Entities.Cities.City()
            {
                Name = cityTextBox.Text,
                Region = regionTextBox.Text
            };

            _context.Cities.Add(newCity);
            _context.SaveChanges();

            City.cityDataGrid.ItemsSource = _context.Cities
                .ToList();

            // Закрыть окно
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно
            this.Close();
        }
    }
}

