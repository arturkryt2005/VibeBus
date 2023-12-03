﻿using System.Linq;
using System.Windows;
using VipeBus.Application.Entities.Cities;
using VipeBus.Core;

namespace VipeBus
{
    public partial class NewCity : Window
    {
        private CityWindow _city;

        private VipeBusContext _context;

        public NewCity(CityWindow city)
        {
            InitializeComponent();

            _city = city;
            _context = new VipeBusContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            City newCity = new City()
            {
                Name = cityTextBox.Text,
                Region = regionTextBox.Text
            };

            _context.Cities.Add(newCity);
            _context.SaveChanges();

            _city.cityDataGrid.ItemsSource = _context.Cities
                .ToList();

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

