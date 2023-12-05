using System;
using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class RouteWindow : Window
    {
        private readonly VipeBusContext _context;

        public RouteWindow()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            routeDataGrid.ItemsSource = _context.Routes.ToList();
        }

        private void NewRouteButton_Click(object sender, RoutedEventArgs e)
        {
            var newRoute = new NewRoute(this)
            {
                Title = "Добавить маршрут"
            };

            newRoute.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (routeDataGrid.SelectedItem != null)
            {
                var selectedRoute = (Application.Entities.Routes.Route)routeDataGrid.SelectedItem;

                selectedRoute.DeparturePoint = null;
                selectedRoute.DestinationPoint = null;

                if (!_context.Routes.Local.Contains(selectedRoute))
                    _context.Routes.Attach(selectedRoute);

                if (MessageBox.Show($"Вы уверены, что хотите удалить маршрут?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Routes.Remove(selectedRoute);
                    _context.SaveChanges();

                    routeDataGrid.ItemsSource = _context.Routes
                        .Include("DeparturePoint")
                        .Include("DestinationPoint")
                        .ToList();
                }
            }
            else
                MessageBox.Show("Выберите маршрут для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Главная"
            };

            headWindow.Show();
            Close();
        }
    }
}
