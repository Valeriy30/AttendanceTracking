using AttendanceTracking.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        public EmployeeList()
        {
            InitializeComponent();
            dgSpecialist.ItemsSource = context.Specialist.ToList();
        }

        private void dgSpecialist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBlock tbChange = dgSpecialist.Columns[0].GetCellContent(dgSpecialist.Items[dgSpecialist.SelectedIndex]) as TextBlock;
                IdChange = Convert.ToInt32(tbChange.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            IdChange = 0;
            var deleteSpecialist = dgSpecialist.SelectedItems.Cast<Specialist>().ToList();
            if (dgSpecialist.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                if (MessageBox.Show($"Вы точно хотите удалить ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        context.Specialist.RemoveRange(deleteSpecialist);
                        context.SaveChanges();
                        MessageBox.Show("Удаленно");
                        dgSpecialist.ItemsSource = context.Specialist.ToList();
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Change = false;
            
            AddEditSpecialistWindow addEditSpecialistWindow = new AddEditSpecialistWindow();
            addEditSpecialistWindow.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgSpecialist.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                Change = true;
                
                AddEditSpecialistWindow addEditSpecialistWindow = new AddEditSpecialistWindow();
                addEditSpecialistWindow.Show();
                this.Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
