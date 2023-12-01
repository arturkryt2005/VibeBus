using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

