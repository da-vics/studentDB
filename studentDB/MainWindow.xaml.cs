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

using System.Configuration;

namespace studentDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentClass students;

        public MainWindow()
        {
            InitializeComponent();
            string connectionstring = ConfigurationManager.ConnectionStrings["studentDB.Properties.Settings.StudentDBConnectionString"].ConnectionString;
            StudentDataClasses1DataContext dataContext = new StudentDataClasses1DataContext(connectionstring);
            students = new StudentClass(dataContext);

            SelectUniversityList.ItemsSource = students.AllUniversity();
            ShowAllStudentsList.ItemsSource = students.displayAllStudents();
            SelectClassList.ItemsSource = students.AllClass();
        }


        /// 
        private void ShowAllStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> studentData = new List<string>();
            try
            {
                var id = ShowAllStudentsList.SelectedValue;
                var spl = id.ToString().Split(' ');
                Student dar = students.showStudentData(spl);


                studentData.Add(dar.FisrtName.ToString());
                studentData.Add(dar.LastName.ToString());
                studentData.Add(students.getUniversity(dar.UniversityId));
                studentData.Add(students.getClass(dar.ClassesId));
            }

            catch (Exception ex)
            {

            }

            finally
            {
                ShowAllStudentsData.ItemsSource = studentData;
            }

        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                students.AddStudent(SelectUniversityList.SelectedValue.ToString(), SelectClassList.SelectedValue.ToString(), AddStudentFirstNameToDB.Text, AddStudentLastNameToDB.Text);
                ShowAllStudentsList.ItemsSource = students.displayAllStudents();

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SelectUniversityList.SelectedValue = string.Empty;
            SelectClassList.SelectedValue = string.Empty;
            AddStudentFirstNameToDB.Text = string.Empty;
            AddStudentLastNameToDB.Text = string.Empty;

        }

        private void RemoveStudentDB_Click(object sender, RoutedEventArgs e)
        {
            students.RemoveStudentFromDB(ShowAllStudentsList.SelectedValue.ToString());
            ShowAllStudentsList.ItemsSource = students.displayAllStudents();
        }
    }
}
