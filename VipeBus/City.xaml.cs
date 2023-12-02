using System.Collections.ObjectModel;
using System.Windows;

namespace VipeBus
{
    public partial class City : Window
    {
        private ObservableCollection<Cities> cities;

        public City()
        {
            InitializeComponent();
            cities = new ObservableCollection<Cities>();
            cityDataGrid.ItemsSource = cities;
        }

        public class Cities
        {
            public string CityName { get; set; }
            public string AdditionalInfo { get; set; } // Новый столбец
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewCity newCity = new NewCity(cities);
            newCity.Title = "Добавить город";
            newCity.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HeadWindow headWindow = new HeadWindow();
            headWindow.Title = "Главная";
            headWindow.Show();
            this.Close();
        }
    }
}

