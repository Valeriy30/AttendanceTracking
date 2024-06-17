using AttendanceTracking.Windows;
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
using static AttendanceTracking.ClassHelper.EFClass;

namespace AttendanceTracking
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

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList employeeList = new EmployeeList();
            this.Close();
            employeeList.Show();
            
        }

        private void btnGroups_Click(object sender, RoutedEventArgs e)
        {
            GroupsList groupsList = new GroupsList();
            this.Close();
            groupsList.ShowDialog();
            
        }

        private void btnStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentList studentList = new StudentList();
            this.Close();
            studentList.ShowDialog();
            
        }

        private void btnAttendance_Click(object sender, RoutedEventArgs e)
        {
            AttendanceWindow attendanceWindow = new AttendanceWindow();
            this.Close();
            attendanceWindow.ShowDialog();

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
