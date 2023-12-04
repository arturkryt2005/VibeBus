﻿using System;
using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Buses;
using VipeBus.Application.Entities.Routes;
using VipeBus.Application.Entities.Trips;
using VipeBus.Application.Entities.Users;
using VipeBus.Core;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для NewTripWindow.xaml
    /// </summary>
    public partial class NewTripWindow : Window
    {
        private VipeBusContext _context;

        private HeadWindow _headWindow;

        public NewTripWindow(HeadWindow headWindow)
        {
            InitializeComponent();

            _headWindow = headWindow;

            _context = new VipeBusContext();

            BusComboBox.ItemsSource = _context.Buses
                .ToList();
            UserComboBox.ItemsSource = _context.Users
                .ToList();
            RouteComboBox.ItemsSource = _context.Routes
                .ToList();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newTrip = new Trip
            {
                Name = TripNameTextBox.Text,
                BusId = ((Bus)BusComboBox.SelectedItem).Id,
                RouteId = ((Route)RouteComboBox.SelectedItem).Id,
                UserId = ((User)UserComboBox.SelectedItem).Id
            };

            try
            {
                _context.Trips.Add(newTrip);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}");
                throw;
            }

            _headWindow.tripDataGrid.ItemsSource = _context.Trips
                    .Include("User")
                    .Include("Bus")
                    .Include("Route")
                    .ToList();

            MessageBox.Show($"Поездка {newTrip.Name} успешно добавлена.");

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}