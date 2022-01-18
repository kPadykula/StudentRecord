using System.Collections.Generic;
using System.Windows;
using DesktopApk.WindowPages;
using System.Linq;
using System;
using System.Windows.Controls;
using System.Data;

namespace DesktopApk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Init Variables
        private int loginID = 0;
        private List<DatabaseModel.Student> myStudents { get; set; }
        private List<DatabaseModel.Lecturer> myLecturers { get; set; }
        private List<DatabaseModel.Subject> mySubjects { get; set; }
        private List<DatabaseModel.Class> myClass { get; set; }
        private List<DatabaseModel.Branch_type> myBranch { get; set; }
        private List<DatabaseModel.Session> mySession { get; set; }
        private List<DatabaseModel.Grade> myGrades { get; set; }

        private List<DatabaseModel.Stipend> myStipend { get; set; }

        public int LoginID { set { loginID = value; } }
        #endregion


        public MainWindow()
        {
            InitializeComponent(); 
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setUserNameOnTop();
        }

        private void setUserNameOnTop()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                var user = context.Admins.FirstOrDefault(Admin => Admin.Id_login == loginID);
                UserInfoLabel.Content = $"Welcome {user.Name}";
            }
        }


        #region CentralButtons and Right Panel Visibility

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            loginID = 0;
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }

        private void btn_Students_Click(object sender, RoutedEventArgs e)
        {
            var lecturersPanel = RightCenterPanelLecturers;
            lecturersPanel.Visibility = Visibility.Collapsed;

            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var mainPanel = RightCenterPanelStudents;
            mainPanel.Visibility = Visibility.Visible;

            loadDataGridStudents();
        }

        private void btn_Lecturers_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Visible;

            loadDataGridLecturers();

        }

        private void btn_Subjects_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Visible;

            LoadDataGridSubjects();
        }

        private void btn_Branch_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Visible;

            LoadDataGridBranch();
        }

        private void btn_Class_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Visible;

            loadDataGridClass();
        }

        private void btn_Session_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Visible;

            loadDataGridSession();
        }

        private void btn_Stipend_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Visible;

            loadDataGridStipend();
        }

        private void btn_Grade_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Collapsed;

            var panelStudent = RightCenterPanelStudents;
            panelStudent.Visibility = Visibility.Collapsed;

            var panelLecturer = RightCenterPanelLecturers;
            panelLecturer.Visibility = Visibility.Collapsed;

            var panelSubjects = RightCenterPanelSubjects;
            panelSubjects.Visibility = Visibility.Collapsed;

            var panelBranch = RightCenterPanelBranch;
            panelBranch.Visibility = Visibility.Collapsed;

            var panelClass = RightCenterPanelClass;
            panelClass.Visibility = Visibility.Collapsed;

            var panelSession = RightCenterPanelSession;
            panelSession.Visibility = Visibility.Collapsed;

            var panelStipend = RightCenterPanelStipend;
            panelStipend.Visibility = Visibility.Collapsed;

            var panelGrades = RightCenterPanelGrades;
            panelGrades.Visibility = Visibility.Visible;

            loadDataGridGrade();
        }
        #endregion

        #region Students Right Panel Controls
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;

            if (tbx.Text != "")
            {
                List<DatabaseModel.Student> filteredList;
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    filteredList = context.Students.Where(x => 
                    (x.First_name.ToLower() + " " + x.Last_name.ToLower() + " " + x.Address.ToLower()
                    ).Contains(tbx.Text.ToLower())).ToList();
                }
                studentDataGrid.ItemsSource = null;
                studentDataGrid.ItemsSource = filteredList;
            }
            else { studentDataGrid.ItemsSource = myStudents; }
        }
        private void loadDataGridStudents()
        {

            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myStudents = context.Students.ToList();
            }

            studentDataGrid.ItemsSource = myStudents;
        }
        private void btn_Add_Student_Click(object sender, RoutedEventArgs e)
        {

            AddNewStudent addNewStudent = new AddNewStudent();
            addNewStudent.Show();
        }

        private void btn_Edit_Student_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = studentDataGrid.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = studentDataGrid.Columns[0].GetCellContent(studentDataGrid.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditStudent editStudent = new EditStudent();
                editStudent.createVarialbeToEdit(parsedId);
                editStudent.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
            
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

                        MessageBox.Show($"You deleted: {student.First_name} {student.Last_name} ");
                    }

                }

            }catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Studnet {error.Message}");
            }
        }
        
        

        private void btn_Refresh_DataGrid(object sender, RoutedEventArgs e)
        {
            loadDataGridStudents();
        }
        #endregion

        #region Lecturers Right Panel Controls
        private void btn_Refresh_DataGrid_Lecturers(object sender, RoutedEventArgs e)
        {
            loadDataGridLecturers();
        }

        private void btn_Add_Lecturer_Click(object sender, RoutedEventArgs e)
        {
            AddNewLecturer addNewLecturer = new AddNewLecturer();
            addNewLecturer.Show();
        }

        private void btn_Edit_Lecturer_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridLecturers.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridLecturers.Columns[0].GetCellContent(dataGridLecturers.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditLecturer editLecturer = new EditLecturer();
                editLecturer.createVarialbeToEdit(parsedId);
                editLecturer.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Delete_Lecturer_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridLecturers.SelectedIndex;
            var id = dataGridLecturers.Columns[0].GetCellContent(dataGridLecturers.SelectedItem) as TextBlock;
            int parsedId = 0;

            try
            {
                parsedId = Int32.Parse(id.Text);
            }
            catch (Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var lecturer = context.Lecturers.FirstOrDefault(Lecturer => Lecturer.Id_lecturer == parsedId);
                    var login = context.Logins.FirstOrDefault(Login => Login.Id_login == lecturer.Id_login);

                    MessageBoxResult result = MessageBox.Show($"You want to delete: {lecturer.First_name} {lecturer.Last_name}\n" +
                        $"With Lecturer ID: {lecturer.Id_lecturer}",
                        "Delete Lecturer",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        context.Lecturers.Attach(lecturer);
                        context.Lecturers.Remove(lecturer);

                        context.Logins.Attach(login);
                        context.Logins.Remove(login);

                        context.SaveChanges();

                        MessageBox.Show($"You deleted: {lecturer.First_name} {lecturer.Last_name} ");
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Lecturer {error.Message}");
            }
        }

        private void loadDataGridLecturers()
        {
            
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myLecturers = context.Lecturers.ToList();
            }

            dataGridLecturers.ItemsSource = myLecturers;
        }
        #endregion

        #region Subject Right Panel Controls
        private void LoadDataGridSubjects()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                mySubjects = context.Subjects.ToList();
            }
            dataGridSubjects.ItemsSource = mySubjects;
        }
        private void btn_Refresh_DataGrid_Subjects(object sender, RoutedEventArgs e)
        {
            LoadDataGridSubjects();
        }

        private void btn_Add_Subjects_Click(object sender, RoutedEventArgs e)
        {
            AddNewSubject addNewSubject = new AddNewSubject();
            addNewSubject.Show();
        }

        private void btn_Edit_Subjects_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridSubjects.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridSubjects.Columns[0].GetCellContent(dataGridSubjects.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditSubject editSubject = new EditSubject();
                editSubject.createVarialbeToEdit(parsedId);
                editSubject.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Delete_Subjects_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridSubjects.SelectedIndex;
            var id = dataGridSubjects.Columns[0].GetCellContent(dataGridSubjects.SelectedItem) as TextBlock;
            int parsedId = 0;

            try
            {
                parsedId = Int32.Parse(id.Text);
            }
            catch (Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var subject = context.Subjects.FirstOrDefault(Subject => Subject.Id_subject == parsedId);

                    MessageBoxResult result = MessageBox.Show($"You want to delete: {subject.Name}\n" +
                        $"With Subject ID: {subject.Id_subject}",
                        "Delete Subject",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        context.Subjects.Attach(subject);
                        context.Subjects.Remove(subject);

                        context.SaveChanges();

                        MessageBox.Show($"You deleted: {subject.Name}");
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Subject {error.Message}");
            }
        }
        #endregion

        #region Branch Right Panel Controls
        private void btn_Delete_Branch_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridBranch.SelectedIndex;
            var id = dataGridBranch.Columns[0].GetCellContent(dataGridBranch.SelectedItem) as TextBlock;
            int parsedId = 0;

            try
            {
                parsedId = Int32.Parse(id.Text);
            }
            catch (Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var branch = context.Branch_type.FirstOrDefault(Branch => Branch.Id_branch == parsedId);

                    MessageBoxResult result = MessageBox.Show($"You want to delete: {branch.Name}\n" +
                        $"With Subject ID: {branch.Id_branch}",
                        "Delete Subject",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        context.Branch_type.Attach(branch);
                        context.Branch_type.Remove(branch);

                        context.SaveChanges();

                        MessageBox.Show($"You deleted: {branch.Name}");
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Branch {error.Message}");
            }
        }

        private void btn_Edit_Branch_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridBranch.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridBranch.Columns[0].GetCellContent(dataGridBranch.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditBranch editBranch = new EditBranch();
                editBranch.createVarialbeToEdit(parsedId);
                editBranch.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Add_Branch_Click(object sender, RoutedEventArgs e)
        {
            AddNewBranch addNewBranch = new AddNewBranch();
            addNewBranch.Show();
        }

        private void btn_Refresh_DataGrid_Branch(object sender, RoutedEventArgs e)
        {
            LoadDataGridBranch();
        }
        private void LoadDataGridBranch()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myBranch = context.Branch_type.ToList();
            }
            dataGridBranch.ItemsSource = myBranch;
            
        }
        #endregion

        #region Class Right Panel Controls

        private void btn_Add_Class_Click(object sender, RoutedEventArgs e)
        {
            AddNewClass addNewClass = new AddNewClass();
            addNewClass.Show();
        }

        private void btn_Edit_Class_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridClass.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridClass.Columns[0].GetCellContent(dataGridClass.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditClass editClass = new EditClass();
                editClass.createVarialbeToEdit(parsedId);
                editClass.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Delete_Class_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridClass.SelectedIndex;
            var id = dataGridClass.Columns[0].GetCellContent(dataGridClass.SelectedItem) as TextBlock;
            int parsedId = 0;

            try
            {
                parsedId = Int32.Parse(id.Text);
            }
            catch (Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var classe = context.Classes.FirstOrDefault(Class => Class.Id_class == parsedId);

                    MessageBoxResult result = MessageBox.Show($"You want to delete: {classe.Name}\n" +
                        $"With Class ID: {classe.Id_class}",
                        "Delete Class",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        context.Classes.Attach(classe);
                        context.Classes.Remove(classe);

                        context.SaveChanges();

                        MessageBox.Show($"You deleted: {classe.Name}");
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Class {error.Message}");
            }
        }

        private void btn_Refresh_DataGrid_Class(object sender, RoutedEventArgs e)
        {
            loadDataGridClass();
        }

        private void loadDataGridClass()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myClass = context.Classes.ToList();
            }
            dataGridClass.ItemsSource = myClass;
        }
        #endregion

        #region Session Right Panel Controls
        private void btn_Delete_Session_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridSession.SelectedIndex;
            int parsedId = 0;
            if (selectedTable != -1) { 
                var id = dataGridSession.Columns[0].GetCellContent(dataGridSession.SelectedItem) as TextBlock;
                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var session = context.Sessions.FirstOrDefault(Session => Session.Id_session == parsedId);

                    if (session != null)
                    {
                        MessageBoxResult result = MessageBox.Show($"You want to delete student {session.Student.First_name} {session.Student.Last_name}\n" +
                        $"session with id: {session.Id_session}",
                        "Delete Session",
                        MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            context.Sessions.Attach(session);
                            context.Sessions.Remove(session);

                            context.SaveChanges();

                            MessageBox.Show($"You deleted session: {session.Id_session}");
                        }
                    }
                    
                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete Session {error.Message}");
            }
        }

        private void btn_Edit_Session_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridSession.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridSession.Columns[0].GetCellContent(dataGridSession.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditSession editSession = new EditSession();
                editSession.createVarialbeToEdit(parsedId);
                editSession.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Add_Session_Click(object sender, RoutedEventArgs e)
        {
            AddNewSession addNewSession = new AddNewSession();
            addNewSession.Show();
        }

        private void btn_Refresh_DataGrid_Session(object sender, RoutedEventArgs e)
        {
            loadDataGridSession();
        }

        private void loadDataGridSession()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                mySession = context.Sessions.ToList();
            }
            dataGridSession.ItemsSource = mySession;
        }
        #endregion

        #region Stipend Right Panel Controls

        private void btn_Refresh_DataGrid_Stipend(object sender, RoutedEventArgs e)
        {
            loadDataGridStipend();
        }

        private void btn_Add_Stipend_Click(object sender, RoutedEventArgs e)
        {
            AddNewStipend addNewStipend = new AddNewStipend();
            addNewStipend.Show();
        }

        private void btn_Edit_Stipend_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridStipend.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridStipend.Columns[0].GetCellContent(dataGridStipend.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditStipend editStipend = new EditStipend();
                editStipend.createVarialbeToEdit(parsedId);
                editStipend.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }
        }

        private void btn_Delete_Stipend_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridStipend.SelectedIndex;
            int parsedId = 0;
            if (selectedTable != -1)
            {
                var id = dataGridStipend.Columns[0].GetCellContent(dataGridStipend.SelectedItem) as TextBlock;
                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var stipend = context.Stipends.FirstOrDefault(Stipend => Stipend.Id_stipend == parsedId);

                    if (stipend != null)
                    {
                        MessageBoxResult result = MessageBox.Show($"You want to delete student {stipend.Student.First_name} {stipend.Student.Last_name}\n" +
                        $"stipend with id: {stipend.Id_stipend} from {stipend.Name}",
                        "Delete stipend",
                        MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            context.Stipends.Attach(stipend);
                            context.Stipends.Remove(stipend);

                            context.SaveChanges();

                            MessageBox.Show($"You deleted stipend: {stipend.Id_stipend}");
                        }
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete stipend {error.Message}");
            }
        }

        private void loadDataGridStipend()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myStipend = context.Stipends.ToList();
            }
            dataGridStipend.ItemsSource = myStipend;
        }
        #endregion

        #region Grades Right Panel Control
        private void btn_Refresh_DataGrid_Grades(object sender, RoutedEventArgs e)
        {
            loadDataGridGrade();
        }

        
        private void btn_Add_Grades_Click(object sender, RoutedEventArgs e)
        {
            AddNewGrade addNewGrade = new AddNewGrade();
            addNewGrade.Show();
        }

        private void btn_Edit_Grades_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridGrades.SelectedIndex;
            if (selectedTable != -1)
            {
                var id = dataGridGrades.Columns[0].GetCellContent(dataGridGrades.SelectedItem) as TextBlock;
                int parsedId = 0;

                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
                EditGrade editGrade = new EditGrade();
                editGrade.createVarialbeToEdit(parsedId);
                editGrade.Show();
            }
            else
            {
                MessageBox.Show("You have to choose which one you want to edit.");
            }

        }

        private void btn_Delete_Grades_Click(object sender, RoutedEventArgs e)
        {
            var selectedTable = dataGridGrades.SelectedIndex;
            int parsedId = 0;
            if (selectedTable != -1)
            {
                var id = dataGridGrades.Columns[0].GetCellContent(dataGridGrades.SelectedItem) as TextBlock;
                try
                {
                    parsedId = Int32.Parse(id.Text);
                }
                catch (Exception ecp)
                {
                    Console.WriteLine("Cannot Parse: " + ecp.Message);
                }
            }

            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var grades = context.Grades.FirstOrDefault(Grades => Grades.Id_grade == parsedId);

                    if (grades != null)
                    {
                        MessageBoxResult result = MessageBox.Show($"You want to delete student {grades.Student.First_name} {grades.Student.Last_name}\n" +
                        $"Grade: {grades.Grade1} from {grades.Lecturer.First_name} {grades.Lecturer.Last_name} subject: {grades.Subject.Name}",
                        "Delete Grade",
                        MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            context.Grades.Attach(grades);
                            context.Grades.Remove(grades);

                            context.SaveChanges();

                            MessageBox.Show($"You deleted grade: {grades.Id_grade}");
                        }
                    }

                }

            }
            catch (Exception error)
            {
                MessageBox.Show($"Cannot delete grade {error.Message}");
            }
        }
        private void loadDataGridGrade()
        {
            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                myGrades = context.Grades.ToList();
            }
            dataGridGrades.ItemsSource = myGrades;
        }
        #endregion

    }
}
