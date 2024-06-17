using AttendanceTracking.DB;
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
using  AttendanceTracking.ClassHelper;


namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для GroupsList.xaml
    /// </summary>
    public partial class GroupsList : Window
    {
        public GroupsList()
        {

            InitializeComponent();
            dgGroup.ItemsSource = context.Group.ToList();
           
                
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            IdChange = 0;
            
            AddEditGroupWindow addEditGroupWindow = new AddEditGroupWindow();
            addEditGroupWindow.Show();
            this.Close();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
             if (dgGroup.SelectedItem == null)
             {
                 MessageBox.Show("Вы не выбрали запись");
             }
             else
             {
                 Change = true;
                 
                 AddEditGroupWindow addEditGroupWindow = new AddEditGroupWindow();
                 addEditGroupWindow.Show();
                this.Close();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            IdChange = 0;
            var deleteGroup = dgGroup.SelectedItems.Cast<Group>().ToList();
            if(dgGroup.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали запись");
            }
            else
            {
                if (MessageBox.Show($"Вы точно хотите удалить запись? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        context.Group.RemoveRange(deleteGroup);
                        context.SaveChanges();
                        MessageBox.Show("Удаленно");
                        dgGroup.ItemsSource = context.Group.ToList();
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }

        private void dgGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBlock tbChange = dgGroup.Columns[0].GetCellContent(dgGroup.Items[dgGroup.SelectedIndex]) as TextBlock;
                IdChange = Convert.ToInt32(tbChange.Text);
            }
            catch (Exception ex) 
            {
                
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
