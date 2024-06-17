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
using static AttendanceTracking.ClassHelper.EFClass;
using AttendanceTracking.DB;
using System.Text.RegularExpressions;
using System.Data.Entity.Migrations;

namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditGroupWindow.xaml
    /// </summary>
    public partial class AddEditGroupWindow : Window
    {
        private DB.Group group = new DB.Group();
        public AddEditGroupWindow()
        {
            InitializeComponent();
            cmbSec.ItemsSource = context.Section.ToList();
            cmbSec.DisplayMemberPath = "Title";
            cmbSec.SelectedIndex = 0;
            if(Change==true)
            {
                tbHeader.Text = "Изменение группы";
                group.Id = context.Group.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Id;
                tbTitleGroup.Text = context.Group.ToList().Where(i => i.Id == IdChange).FirstOrDefault().Title;
                cmbSec.SelectedIndex = context.Group.ToList().Where(i => i.Id == IdChange).FirstOrDefault().IdSection -1;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            GroupsList groupsList = new GroupsList();
            groupsList.Show();
            Change = false;
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitleGroup.Text))
            {
                MessageBox.Show("Название не может быть пустым");
                return;
            }
            if (cmbSec.SelectedItem.Equals(null))
            {
                MessageBox.Show("Вы не выбрали секцию");
                return;
            }
            group.Id = IdChange;
            group.Title = tbTitleGroup.Text;
            group.IdSection = (cmbSec.SelectedItem as DB.Section).Id;
            context.Group.AddOrUpdate(group);
            context.SaveChanges();
            GroupsList groupsList = new GroupsList();
            groupsList.Show();
            this.Close();

        }
    }
}
