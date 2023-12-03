using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VipeBus.Core;
using System;



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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (cityDataGrid.SelectedItem != null)
            {
                var selectedCity = (Application.Entities.Cities.City)cityDataGrid.SelectedItem;

                selectedCity.Name = null;

                if (!_context.Cities.Local.Contains(selectedCity))
                {
                    _context.Cities.Attach(selectedCity);
                }

                if (MessageBox.Show($"Вы уверены, что хотите удалить город {selectedCity.Name} ?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Cities.Remove(selectedCity);
                    _context.SaveChanges();

                    cityDataGrid.ItemsSource = _context.Cities.Include("Name").ToList();
                }
            }
            else
                MessageBox.Show("Выберите водителя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

