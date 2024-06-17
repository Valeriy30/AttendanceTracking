using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using AttendanceTracking.DB;
using static AttendanceTracking.ClassHelper.EFClass;

namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var authUser = context.AuthData.ToList()
                .Where(i => i.Login == tbLogin.Text && i.Pass == pbPass.Password)
                .FirstOrDefault();

            if (authUser != null)
            {
                IdAuth = Convert.ToInt32(authUser.IdSpecialist);
                if (context.Role.Where(i => i.Id == context.RoleSpecialist.Where(o => o.IdSpecialist == IdAuth).FirstOrDefault().IdRole).FirstOrDefault().Title != "Администратор")
                {
                    AttendanceWindow attendanceWindow = new AttendanceWindow();
                    attendanceWindow.Show();
                    this.Close();
                }
                else 
                { 
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
                }
                
            }
            else
            {
                MessageBox.Show($"Логин или пароль введены не правильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
