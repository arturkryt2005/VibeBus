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
        public partial class NewRoute : Window
    {
            private ObservableCollection<Route.Routes> routes;

            public NewRoute(ObservableCollection<Route.Routes> routes)
            {
                InitializeComponent();
                this.routes = routes;
            fromTextBox.Text = "Откуда";
            toTextBox.Text = "Куда";
            distanceTextBox.Text = "Растояние";
             }
            

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
            if (int.TryParse(distanceTextBox.Text, out int distance))
            {
                Route.Routes newRoute = new Route.Routes
                {
                    City = fromTextBox.Text,
                    City2 = toTextBox.Text,
                    Distance = distance
                };

                routes.Add(newRoute);

                // Закрыть окно
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректное значение для расстояния.");
            }
            }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно
            this.Close();
        }
    }
}

