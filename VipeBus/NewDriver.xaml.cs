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
    public partial class NewDriver : Window
    {
        private ObservableCollection<Driver.Drivers> drivers;

        public NewDriver(ObservableCollection<Driver.Drivers> drivers)
        {
            InitializeComponent();
            this.drivers = drivers;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Driver.Drivers newDriver = new Driver.Drivers
            {
                LastName = LastNameTextBox.Text,
                FirstName = FirstNameTextBox.Text,
                MiddleName = MiddleNameTextBox.Text,
                BusNumber = BusNumberTextBox.Text
            };

            drivers.Add(newDriver);

            // Закрыть окно
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть окно
            this.Close();
        }
    }
}
