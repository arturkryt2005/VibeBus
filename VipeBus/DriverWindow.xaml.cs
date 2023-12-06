using System;
using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{

    public partial class DriverWindow : Window
    {
        private VipeBusContext _context;

        public DriverWindow()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            driverDataGrid.ItemsSource = _context.Drivers.Include("Bus").ToList();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Маршруты"
            };

            headWindow.Show();
            Close();
        }

        private NewDriver newDriver;
        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            if (newDriver == null || !newDriver.IsVisible)
            {
                newDriver = new NewDriver(this)
                {
                    Title = "Добавить водителя"
                };
                newDriver.Closed += (s, args) => newDriver = null;
                newDriver.Show();
            }
            else
            {
                newDriver.Activate();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (driverDataGrid.SelectedItem != null)
            {
                var selectedDriver = (Application.Entities.Drivers.Driver)driverDataGrid.SelectedItem;


                if (_context.Routes.Any(route => route.Driver.Id == selectedDriver.Id || route.Driver.Id == selectedDriver.Id))
                {
                    MessageBox.Show("Нельзя удалить водителя, который задействован в маршруте.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                selectedDriver.Bus = null;

                if (!_context.Drivers.Local.Contains(selectedDriver))
                    _context.Drivers.Attach(selectedDriver);

                if (MessageBox.Show($"Вы уверены, что хотите удалить водителя {selectedDriver.FirstName} {selectedDriver.LastName}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Drivers.Remove(selectedDriver);
                    _context.SaveChanges();

                    driverDataGrid.ItemsSource = _context.Drivers.Include("Bus").ToList();
                }
            }
            else
                MessageBox.Show("Выберите водителя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose();
        }
    }
}


