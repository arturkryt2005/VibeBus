using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
        
            // Проверка логина и пароля
            if (UserLog.Text == "user" && UserPas.Text == "password")
            {
                MessageBox.Show("Вход выполнен успешно!");

                // Открываем новое окно или выполняем другие действия при успешной авторизации
                HeadWindow headWindow = new HeadWindow();
                headWindow.Title = "VipeBus";
                headWindow.Show();
                this.Close();
                // Закрываем текущее окно
                Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }
    }
}
