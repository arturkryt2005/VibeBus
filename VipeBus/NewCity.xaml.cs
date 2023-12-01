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
    public partial class NewCity : Window
    {
        private ObservableCollection<City.Cities> cities;

        public NewCity(ObservableCollection<City.Cities> cities)
        {
            InitializeComponent();
            this.cities = cities;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            City.Cities newCity = new City.Cities
            {
                CityName = cityTextBox.Text,
                AdditionalInfo = "Дополнительная информация" // Можно изменить на нужное вам значение
            };

            cities.Add(newCity);

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

