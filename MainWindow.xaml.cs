using System.Collections.Generic;
using System.Windows;
using DesktopApk.WindowPages;
using System.Linq;
using System;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;

namespace DesktopApk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int loginID = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public int LoginID { set { loginID = value; }  }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Students_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Hidden;

            var mainPanel = RightCenterPanelStudents;
            mainPanel.Visibility = Visibility.Visible;

            ComboBoxDataBindingBranch();
        }

        private void btn_Lecturers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Subjects_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Branch_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ComboBoxDataBindingBranch()
        {
            var itemBox = comboBoxSelectBranch;
            List<DatabaseModel.Branch_type> list = new List<DatabaseModel.Branch_type>();
            
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                foreach (var item in context.Branch_type)
                {
                    list.Add(item);
                }
                itemBox.ItemsSource = list;
                itemBox.SelectedValuePath = "Id_branch";
                itemBox.DisplayMemberPath = "Name";
            }
        }

        private void btn_Add_Student_Click(object sender, RoutedEventArgs e)
        {
            AddNewStudent addNewStudent = new AddNewStudent();
            addNewStudent.Show();

        }

        private void btn_Edit_Student_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Delete_Student_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = studentDataGrid.SelectedIndex;
            var id = studentDataGrid.Columns[0].GetCellContent(studentDataGrid.SelectedItem) as TextBlock;
            int parsedId = 0;
            
            try
            {
                parsedId = Int32.Parse(id.Text);
            }catch(Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var student = context.Students.FirstOrDefault(Student => Student.Id_student == parsedId);
                    var login = context.Logins.FirstOrDefault(Login => Login.Id_login == student.Id_login);

                    MessageBoxResult result = MessageBox.Show($"You want to delete: {student.First_name} {student.Last_name}\n" +
                        $"With Student_ID: {student.Id_student}", 
                        "Delete Student", 
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        context.Students.Attach(student);
                        context.Students.Remove(student);

                        context.Logins.Attach(login);
                        context.Logins.Remove(login);

                        context.SaveChanges();
                        RefreshGridData();

                        MessageBox.Show($"You deleted: {student.First_name} {student.Last_name} ");
                    }

                }

            }catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGridData();
        }

        private void RefreshGridData()
        {
            DatabaseSchoolDataSet databaseSchoolDataSet = ((DatabaseSchoolDataSet)(this.FindResource("databaseSchoolDataSet")));
            // Load data into the table Student. You can modify this code as needed.
            DatabaseSchoolDataSetTableAdapters.StudentTableAdapter databaseSchoolDataSetStudentTableAdapter = new DatabaseSchoolDataSetTableAdapters.StudentTableAdapter();
            databaseSchoolDataSetStudentTableAdapter.Fill(databaseSchoolDataSet.Student);
            CollectionViewSource studentViewSource = ((CollectionViewSource)(this.FindResource("studentViewSource")));
            studentViewSource.View.MoveCurrentToFirst();
        }

        private void btn_Refresh_DataGrid(object sender, RoutedEventArgs e)
        {
            RefreshGridData();
        }
    }
}
