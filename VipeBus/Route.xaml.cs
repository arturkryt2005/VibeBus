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
    public partial class Route : Window
    {
        private ObservableCollection<Routes> routes;

        public Route()
        {
            InitializeComponent();
            routes = new ObservableCollection<Routes>();
            routeDataGrid.ItemsSource = routes;
        }

        public class Routes
        {
            public string City { get; set; }
            public string City2 { get; set; }
            public int Distance { get; set; }
        }

        private void NewRouteButton_Click(object sender, RoutedEventArgs e)
        {
            NewRoute newRoute = new NewRoute(routes);
            newRoute.Title = "Добавить маршрут";
            newRoute.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HeadWindow headWindow = new HeadWindow();
            headWindow.Title = "Главная";
            headWindow.Show();
            this.Close();
        }
    }
}
