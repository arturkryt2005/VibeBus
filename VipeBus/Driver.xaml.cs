﻿using System;
using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class Driver : Window
    {
        private VipeBusContext _context;

        public Driver()
        {
            InitializeComponent();

            _context = new VipeBusContext();
            var result = _context.Drivers.Include("Bus").ToList();
            tripDataGrid.ItemsSource = result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var headWindow = new HeadWindow
            {
                Title = "Маршруты"
            };
            headWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newdriver = new NewDriver(this)
            {
                Title = "Добавить водителя"
            };
            newdriver.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (tripDataGrid.SelectedItem != null)
            {
                var selectedDriver = (Application.Entities.Drivers.Driver)tripDataGrid.SelectedItem;

                selectedDriver.Bus = null;

                if (!_context.Drivers.Local.Contains(selectedDriver))
                {
                    _context.Drivers.Attach(selectedDriver);
                }

                if (MessageBox.Show($"Вы уверены, что хотите удалить водителя {selectedDriver.FirstName} {selectedDriver.LastName}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Drivers.Remove(selectedDriver);
                    _context.SaveChanges();

                    // Refresh the DataGrid
                    tripDataGrid.ItemsSource = _context.Drivers.Include("Bus").ToList();
                }
            }
            else
            {
                MessageBox.Show("Выберите водителя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose(); // Dispose the context when the window is closed
        }
    }
}


