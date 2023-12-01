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
using static VipeBus.Driver;

namespace VipeBus
{
    public partial class Driver : Window
    {
        private ObservableCollection<Drivers> drivers;


        public Driver()
        {
            InitializeComponent();
            drivers = new ObservableCollection<Drivers>(); // Также изменено на ObservableCollection<Driver>
            tripDataGrid.ItemsSource = drivers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HeadWindow headWindow = new HeadWindow();
            headWindow.Title = "Маршруты";
            headWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewDriver newdriver = new NewDriver(drivers);
            newdriver.Title = "Добавить водителя";
            newdriver.Show();
        }
        public class Drivers
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string BusNumber { get; set; }
        }
    }
}


