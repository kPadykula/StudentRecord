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
    /// Interaction logic for EditLecturer.xaml
    /// </summary>
    public partial class EditLecturer : Window
    {
        private String firstName { get; set; }
        private String lastName { get; set; }
        private String address { get; set; }
        private int lecturerID { get; set; }
        private int loginID { get; set; }
        public EditLecturer()
        {
            InitializeComponent();
        }

        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Lecturer oldLecturer = new DatabaseModel.Lecturer
            {
                Id_lecturer = lecturerID,
                First_name = firstName,
                Last_name = lastName,
                Address = address
            };

            DatabaseModel.Lecturer newLecturer = new DatabaseModel.Lecturer
            {
                Id_lecturer = lecturerID,
                First_name = FirstNameEdit.Text,
                Last_name = LastNameEdit.Text,
                Address = AddressEdit.Text
            };
            if (oldLecturer.Equals(newLecturer))
            {
                MessageBox.Show("Lecturer jest taki sam");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var result = context.Lecturers.SingleOrDefault(Lecturer => Lecturer.Id_lecturer == oldLecturer.Id_lecturer);
                    if (result != null)
                    {
                        try
                        {
                            result.First_name = newLecturer.First_name;
                            result.Last_name = newLecturer.Last_name;
                            result.Address = newLecturer.Address;
                           

                            context.SaveChanges();
                            MessageBox.Show($"Lecturer has been changed");
                            this.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show($"Error: {exp.Message}");
                        }
                    }
                }
            }
        }

        public void createVarialbeToEdit(int id)
        {
            lecturerID = id;
            DatabaseModel.Lecturer lecturer = findLecturer(id);

            if (lecturer != null)
            {
                firstName = lecturer.First_name;
                lastName = lecturer.Last_name;
                address = lecturer.Address;
                loginID = lecturer.Id_login;


                FirstNameEdit.Text = firstName;
                LastNameEdit.Text = lastName;
                AddressEdit.Text = address;

            }
        }
        private DatabaseModel.Lecturer findLecturer(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Lecturer lecturerToFind = new DatabaseModel.Lecturer();
                    lecturerToFind = context.Lecturers.FirstOrDefault(Lecturer => Lecturer.Id_lecturer == lecturerID);
                    return lecturerToFind;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }


    }
}
