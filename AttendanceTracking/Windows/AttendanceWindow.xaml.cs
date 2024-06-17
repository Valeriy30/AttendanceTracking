using AttendanceTracking.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
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

namespace AttendanceTracking.Windows
{
    /// <summary>
    /// Логика взаимодействия для AttendanceWindow.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        public int selectedGroupId;
       
        
        public AttendanceWindow()
        {
            InitializeComponent();
            if (context.Role.Where(i => i.Id == context.RoleSpecialist.Where(o => o.IdSpecialist == IdAuth).FirstOrDefault().IdRole).FirstOrDefault().Title != "Администратор")
            {
               
                    List<SpecialistGroup> groupSpec = new List<SpecialistGroup>();
                    groupSpec = context.SpecialistGroup.ToList();
                    List<SpecialistGroup> sortGS = new List<SpecialistGroup>();
                    foreach(SpecialistGroup specgroup in groupSpec)
                    {
                        if(specgroup.IdSpecialist==IdAuth)
                        {
                            sortGS.Add(specgroup);
                        }
                    }
                    List<Group> groups = new List<Group>();
                foreach (SpecialistGroup specialistGroup in sortGS)
                {
                    
                    groups.Add(context.Group.ToList().Where(i => i.Id == specialistGroup.IdGroup).FirstOrDefault());
                }
                LvGroupList.ItemsSource = groups;
            }
            else
            {
                LvGroupList.ItemsSource = context.Group.ToList();
            }
            List<Student> students = new List<Student>();
            students = context.Student.ToList();

            foreach (Student student in students)
            {
                student.FirstLastName = student.FirstName + " " + student.LastName;

            }
            LvSheduleList.ItemsSource = null;
        }
       
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (context.Role.Where(i => i.Id == context.RoleSpecialist.Where(o => o.IdSpecialist == IdAuth).FirstOrDefault().IdRole).FirstOrDefault().Title != "Администратор")
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                LvSheduleList.DataContext = null;

                // Очистка ItemsSource
                LvSheduleList.ItemsSource = null;
                this.Close();
               
                
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        
        private void groupsClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var selectedGroup = btn.DataContext as Group;
            if (selectedGroup != null)
            {
                List<Student> students = new List<Student>();
                students = context.Student.ToList();
                List<Student> sortStudent = new List<Student>();
                foreach (Student student in students)
                {

                    foreach (GroupStudent groupStudent in context.GroupStudent)
                    {
                        if (student.Id == groupStudent.IdStudent && groupStudent.IdGroup == selectedGroup.Id)
                        {
                            sortStudent.Add(student);

                        }
                    }
                }
                LvStudentList.ItemsSource = sortStudent;
                
            }
            selectedGroupId = selectedGroup.Id;
        }
   
        private void studentClick (object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var selectedStudent = btn.DataContext as Student;
            if (selectedStudent != null)
            {
                var data = context.StudentShedule.ToList();
                ObservableCollection<StudentShedule> sheduleStudents = new ObservableCollection<StudentShedule>(data) ;
                ObservableCollection<StudentShedule> sortStudentShedule = new ObservableCollection<StudentShedule>();
                foreach (StudentShedule studentShedule in sheduleStudents)
                {
                    foreach (Shedule shedule in context.Shedule)
                    {
                        shedule.Date = shedule.DateStart.ToString("g");
                        if (studentShedule.IdShedule == shedule.Id && studentShedule.IdStudent==selectedStudent.Id)
                        {
                            sortStudentShedule.Add(studentShedule);
                        }
                    }
                }
                LvSheduleList.ItemsSource = sortStudentShedule;
                
            }
        }
        

        private void ScoreBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var score = textBox.Text;
            if (score.ToString() != "2" && score.ToString() != "3" && score.ToString() != "4" && score.ToString() != "5" && score.ToString() != "н" && score.ToString() != "")
            {
                textBox.Text = "";
                MessageBox.Show("Можно ставить только оценки от 2 до 5 и 'н'(не был на занятии) ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<StudentShedule> finalStudenrShed = new List<StudentShedule>();
            finalStudenrShed = LvSheduleList.ItemsSource.Cast<StudentShedule>().ToList();
            for (int i = 0; i < finalStudenrShed.Count; i++)
            {
                context.StudentShedule.AddOrUpdate(finalStudenrShed[i]);
            }

            context.SaveChanges();

        }
    }
}
