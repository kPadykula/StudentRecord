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

namespace DesktopApk.WindowPages
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window
    {
        private String firstName { get; set; }
        private String lastName { get; set; }
        private String address { get; set; }
        private DateTime birthDate { get; set; }
        private DateTime entranceDate { get; set; }
        private Nullable<DateTime> leavingDate { get; set; }
        private Nullable<bool> stipend { get; set; }
        private int studentID { get; set; }
        private int loginID { get; set; }

        public EditStudent()
        {
            InitializeComponent();
        }

        public void createVarialbeToEdit(int id)
        {
            studentID = id;
            DatabaseModel.Student student = findStudent(id);
            
            if (student != null)
            {
                firstName = student.First_name;
                lastName = student.Last_name;
                address = student.Address;
                birthDate = student.Birth_date;
                entranceDate = student.Entrance_date;
                leavingDate = student.Leaving_date;
                stipend = student.Stipend != true ? false : true;
                loginID = student.Id_login;
                

                FirstNameEdit.Text = firstName;
                LastNameEdit.Text = lastName;
                AddressEdit.Text = address;
                BirthDateEdit.SelectedDate = birthDate;
                EntranceDateEdit.SelectedDate = entranceDate;
                LeavingDateEdit.SelectedDate = leavingDate;
                if (stipend != null && stipend == true) ComboboxStipendEdit.SelectedIndex = 0;
                else ComboboxStipendEdit.SelectedIndex = 1;

            }

        }

        private DatabaseModel.Student findStudent(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Student studentToFind = new DatabaseModel.Student();
                    studentToFind = context.Students.FirstOrDefault(Student => Student.Id_student == studentID);
                    return studentToFind;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }
        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Student oldStudent = new DatabaseModel.Student {
                Id_student = studentID,
                First_name = firstName,
                Last_name = lastName,
                Address = address,
                Birth_date = birthDate,
                Entrance_date = entranceDate,
                Leaving_date = leavingDate,
                Stipend = stipend
            };

            DatabaseModel.Student newStudent = new DatabaseModel.Student {
                Id_student = studentID,
                First_name = FirstNameEdit.Text,
                Last_name = LastNameEdit.Text,
                Address = AddressEdit.Text,
                Birth_date = (DateTime)BirthDateEdit.SelectedDate,
                Entrance_date = (DateTime)EntranceDateEdit.SelectedDate,
                Leaving_date = LeavingDateEdit.SelectedDate != null ? leavingDate : null,
                Stipend = ComboboxStipendEdit.SelectedIndex == 0 ? true : false
            };
            if (oldStudent.Equals(newStudent))
            {
                MessageBox.Show("Student jest taki sam");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var result = context.Students.SingleOrDefault(Student => Student.Id_student == oldStudent.Id_student);
                    if (result != null)
                    {
                        try
                        {
                            result.First_name = newStudent.First_name;
                            result.Last_name = newStudent.Last_name;
                            result.Address = newStudent.Address;
                            result.Birth_date = newStudent.Birth_date;
                            result.Entrance_date = newStudent.Entrance_date;
                            result.Leaving_date = newStudent.Leaving_date;
                            result.Stipend = newStudent.Stipend;

                            context.SaveChanges();
                            MessageBox.Show($"Student has been changed");
                            this.Close();
                        }catch(Exception exp)
                        {
                            MessageBox.Show($"Error: {exp.Message}");
                        }
                    }
                }
            }
        }
    }
}
