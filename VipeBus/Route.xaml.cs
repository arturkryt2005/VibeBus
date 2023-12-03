using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class Route : Window
    {
        private readonly VipeBusContext _context;

        public Route()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Главная"
            };
            headWindow.Show();
            this.Close();
        }
    }
}
