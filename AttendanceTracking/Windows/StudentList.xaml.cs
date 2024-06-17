using System;
using System.Collections.Generic;
using System.Linq;
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
using AttendanceTracking.DB;
using static AttendanceTracking.ClassHelper.EFClass;

namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для StudentList.xaml
    /// </summary>
    public partial class StudentList : Window
    {
        public StudentList()
        {
            InitializeComponent();
           
            dgStudent.ItemsSource = context.Student.ToList();
            
        }

        private void dgStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBlock tbChange = dgStudent.Columns[0].GetCellContent(dgStudent.Items[dgStudent.SelectedIndex]) as TextBlock;
                IdChange = Convert.ToInt32(tbChange.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            
            var deleteStudent = dgStudent.SelectedItems.Cast<Student>().ToList();
            if (dgStudent.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                if (MessageBox.Show($"Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        context.Student.RemoveRange(deleteStudent);
                        context.SaveChanges();
                        MessageBox.Show("Удаленно");
                        dgStudent.ItemsSource = context.Student.ToList();
                        IdChange = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());

                    }
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudent.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                Change = true;
                
                AddEditStudentWindow addEditStudentWindow = new AddEditStudentWindow();
                addEditStudentWindow.Show();
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            IdChange = 0;
            
            AddEditStudentWindow addEditStudentWindow = new AddEditStudentWindow();
            addEditStudentWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();
        }

        private void btnAddtoGroup_Click(object sender, RoutedEventArgs e)
        {
           
            var deleteStudent = dgStudent.SelectedItems.Cast<Student>().ToList();
            if (dgStudent.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                AddToGroup addToGroup = new AddToGroup();
                addToGroup.Show();
                this.Close();
            }
        }
    }
}
