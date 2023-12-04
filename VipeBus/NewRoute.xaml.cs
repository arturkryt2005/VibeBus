﻿using System;
using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewRoute : Window
    {
        private VipeBusContext _context;

        private RouteWindow _route;

        public NewRoute(RouteWindow route)
        {
            InitializeComponent();

            _context = new VipeBusContext();

            FromComboBox.ItemsSource = _context.Cities.ToList();
            toComboBox.ItemsSource = _context.Cities.ToList();
            _route = route;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromComboBox.SelectedItem == null) 
                {
                MessageBox.Show("Заполните место отпраления.");
                return;
            }

                if (toComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните место назначения.");
                return;
            }

                if (departureTimePicker.Value == null)
            {
                MessageBox.Show("Заполните время отправления.");
                return;
            }

                if (arrivalTimePicker.Value == null)
                  {
                MessageBox.Show("Заполните время приезда.");
                return;
            }

                if (departureDatePicker.SelectedDate == null)
                  {
                MessageBox.Show("Заполните дату отправления.");
                return;
            }

                if (arrivalDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните дату приезда.");
                return;
            }

            using (_context = new VipeBusContext())
            {
                var newRoute = new Application.Entities.Routes.Route()
                {
                    DeparturePointId = ((Application.Entities.Cities.City)FromComboBox.SelectedItem).Id,
                    DestinationPointId = ((Application.Entities.Cities.City)toComboBox.SelectedItem).Id,
                    DepartureTime = new DateTime(departureDatePicker.SelectedDate.Value.Year,
                        departureDatePicker.SelectedDate.Value.Month, 
                        departureDatePicker.SelectedDate.Value.Day, 
                        departureTimePicker.Value.Value.Hour, 
                        departureTimePicker.Value.Value.Minute, 
                        departureTimePicker.Value.Value.Second),
                    DestinationTime = new DateTime(arrivalDatePicker.SelectedDate.Value.Year,
                        arrivalDatePicker.SelectedDate.Value.Month, 
                        arrivalDatePicker.SelectedDate.Value.Day,
                        arrivalTimePicker.Value.Value.Hour, 
                        arrivalTimePicker.Value.Value.Minute, 
                        arrivalTimePicker.Value.Value.Second),
                };

                try
                {
                    _context.Routes.Add(newRoute);
                    _context.SaveChanges();

                    _route.routeDataGrid.ItemsSource = _context.Routes
                        .Include("DeparturePoint")
                        .Include("DestinationPoint")
                        .ToList();

                    MessageBox.Show($"Маршрут {((Application.Entities.Cities.City)FromComboBox.SelectedItem).Name} - {((Application.Entities.Cities.City)toComboBox.SelectedItem).Name} успешно добавлен.");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

