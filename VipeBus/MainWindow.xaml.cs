using System.Linq;
using System.Windows;
using VipeBus.Core;

namespace VipeBus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VipeBusContext _context;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (_context = new VipeBusContext())
            {
                foreach (var user in _context.Users.ToList())
                {
                    if (UserLog.Text == user.Name && UserPas.Text == user.Password)
                    {
                        MessageBox.Show("Вход выполнен успешно!");

                        // Открываем новое окно или выполняем другие действия при успешной авторизации
                        HeadWindow headWindow = new HeadWindow();
                        headWindow.Title = "VipeBus";
                        headWindow.Show();
                        this.Close();
                        // Закрываем текущее окно
                        Close();

                        return;
                    }
                }

                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }
    }
}
