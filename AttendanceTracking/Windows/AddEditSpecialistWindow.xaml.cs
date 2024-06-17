using AttendanceTracking.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static AttendanceTracking.ClassHelper.EFClass;

namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditSpecialistWindow.xaml
    /// </summary>
    public partial class AddEditSpecialistWindow : Window
    {
        private Specialist specialist = new Specialist();
        public AddEditSpecialistWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = context.Gender.ToList();
            cmbGender.DisplayMemberPath = "Title";
            dpBirthDay.DisplayDateEnd = DateTime.Now.AddYears(-18);
            if (Change==true)
            {
                tbHeader.Text ="Изменение сотрудника";
                tbFname.Text = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().FirstName;
                tbLname.Text = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().LastName;
                tbPatronymic.Text = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Patronymic;
                dpBirthDay.SelectedDate = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Birthday;
                cmbGender.SelectedIndex = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().IdGender-1;
                tbEmail.Text = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Email;
                tbPhone.Text = context.Specialist.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Phone;
                
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(tbFname.Text))
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLname.Text))
            {
                MessageBox.Show("Фамилия не может быть пустой");
                return;
            }
            if (string.IsNullOrWhiteSpace(dpBirthDay.Text))
            {

                MessageBox.Show("Дата не может быть пустой");
                return;
            }
            if (cmbGender.SelectedItem.Equals(null))
            {
                MessageBox.Show("Выберете пол");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Почта не может быть пустой");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Телефон не может быть пустым");
                return;
            }
            if (!tbFname.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Имя введено не корректно");
                return;
            }
            if (!tbLname.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Фамилия введена не корректно");
                return;
            }
            if (!tbPatronymic.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Отчество введено не корректно");
                return;
            }
            if (!tbPhone.Text.All(Char.IsDigit))
            {
                MessageBox.Show("Номер телефона введен не корректно");
                return;
            }
            if (Convert.ToInt64(tbPhone.Text) < 70000000000 || Convert.ToInt64(tbPhone.Text) > 89999999999)
            {
                MessageBox.Show("Номер телефона введен не корректно");
                return;
            }
            bool isValid = Regex.IsMatch(tbEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!isValid)
            {
                MessageBox.Show("Почта введена не корректно");
                return;
            }
            specialist.Id = IdChange;
             specialist.FirstName = tbFname.Text;
             specialist.LastName = tbLname.Text;
             specialist.Patronymic = tbPatronymic.Text;
             specialist.Birthday = dpBirthDay.SelectedDate.Value;
             specialist.Email = tbEmail.Text;
             specialist.Phone = tbPhone.Text;
             specialist.IdGender = (cmbGender.SelectedItem as Gender).Id;
             context.Specialist.AddOrUpdate(specialist);
             context.SaveChanges();
             Change = false;
           
            EmployeeList employeeList = new EmployeeList();
            employeeList.Show();
            this.Close();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            EmployeeList employeeList = new EmployeeList();
            employeeList.Show();
            Change = false;
            this.Close();
        }
    }
}
