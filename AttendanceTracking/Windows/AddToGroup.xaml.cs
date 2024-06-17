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
using static AttendanceTracking.ClassHelper.EFClass;
using AttendanceTracking.DB;


namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddToGroup.xaml
    /// </summary>
    public partial class AddToGroup : Window
    {
        public static int IdGroupStudent=0;
        public AddToGroup()
        {
            InitializeComponent();
            LvGroupList.ItemsSource = context.GroupStudent.Where(i => i.IdStudent == IdChange).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            IdChange = 0;
            StudentList studentList = new StudentList();
            studentList.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            IdGroupStudent = 0;
            AddEditToGroup addEditToGroup = new AddEditToGroup();
            addEditToGroup.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (IdGroupStudent != 0)
            {
                AddEditToGroup addEditToGroup = new AddEditToGroup();
                addEditToGroup.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали запись","Вниамание",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (IdGroupStudent != 0)
            {
                if (MessageBox.Show($"Вы точно хотите удалить ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var deleteGroup = context.GroupStudent.Where(i => i.Id == IdGroupStudent).FirstOrDefault() as GroupStudent;
                    context.GroupStudent.Remove(deleteGroup);
                    context.SaveChanges();
                    LvGroupList.ItemsSource = context.GroupStudent.Where(i=> i.IdStudent==IdChange).ToList();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали запись", "Вниамание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void SelectGroup_Click(object sender, RoutedEventArgs e)
        {
            Button btnSelect = sender as Button;
            var selectedGroup = btnSelect.DataContext as GroupStudent;
           IdGroupStudent = selectedGroup.Id;
           


        }

        private void SelectGroup_GotFocus(object sender, RoutedEventArgs e)
        {
            Button btnSelect = sender as Button;
            btnSelect.Background = Brushes.Red;
            btnSelect.Foreground = Brushes.White;
        }

        private void SelectGroup_LostFocus(object sender, RoutedEventArgs e)
        {
            Button btnSelect = sender as Button;
            btnSelect.Background = Brushes.White;
            btnSelect.Foreground = Brushes.Black;
        }
    }
}
