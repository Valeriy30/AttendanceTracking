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
using System.Windows.Shapes;
using static AttendanceTracking.Windows.AddToGroup;
using static AttendanceTracking.ClassHelper.EFClass;
using AttendanceTracking.DB;
using System.Data.Entity.Migrations;


namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditToGroup.xaml
    /// </summary>
    public partial class AddEditToGroup : Window
    {
        private GroupStudent groupStudent = new GroupStudent();
        public AddEditToGroup()
        {
            InitializeComponent();
            cmbGroup.ItemsSource = context.Group.ToList();
            cmbGroup.DisplayMemberPath = "Title";
            if(IdGroupStudent!=0)
            {
                tbHeader.Text = "Изменение записи";
                cmbGroup.SelectedIndex=context.GroupStudent.Where(i => i.Id==IdGroupStudent).FirstOrDefault().IdGroup-1;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AddToGroup addToGroup = new AddToGroup();
            addToGroup.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbGroup.SelectedItem.Equals(null))
            {
                MessageBox.Show("Выберете группу");
                return;
            }
            groupStudent.Id= IdGroupStudent;
            groupStudent.IdGroup = (cmbGroup.SelectedItem as Group).Id;
            groupStudent.IdStudent = IdChange;
            context.GroupStudent.AddOrUpdate(groupStudent);
            context.SaveChanges();
            AddToGroup addToGroup = new AddToGroup();
            addToGroup.Show();  
            this.Close();
        }
    }
}
