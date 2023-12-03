using System.Collections.ObjectModel;
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
            NewCity newCity = new NewCity(this)
            {
                Title = "Добавить город"
            };
            newCity.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HeadWindow headWindow = new HeadWindow
            {
                Title = "Главная"
            };
            headWindow.Show();
            this.Close();
        }
    }
}

