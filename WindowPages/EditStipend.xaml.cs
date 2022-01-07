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
    /// Interaction logic for EditStipend.xaml
    /// </summary>
    public partial class EditStipend : Window
    {
        private int idStipend { get; set; }
        private int idStudentVariable { get; set; }
        private string NameVariable { get; set; }
        private DateTime startDateVariable { get; set; }
        private DateTime? endDateVariable { get; set; }

        private int idStudentVariableNew { get; set; }

        public EditStipend()
        {
            InitializeComponent();
        }

        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Stipend oldStipend = new DatabaseModel.Stipend
            {
                Id_stipend = idStipend,
                Id_student = idStudentVariable,
                Name = NameVariable,
                Start_date = startDateVariable,
                End_date = endDateVariable
            };
            try
            {
                idStudentVariableNew = Int32.Parse(StudentIDNew.Text);
            }
            catch (Exception exep)
            {
                MessageBox.Show($"Error : {exep.Message}");
            }

            DatabaseModel.Stipend newStipend = new DatabaseModel.Stipend
            {
                Id_stipend = idStipend,
                Id_student = idStudentVariableNew,
                Name = NameNew.Text,
                Start_date = (DateTime)StartDateNew.SelectedDate,
                End_date = (DateTime)EndDateNew.SelectedDate
            };
            if (oldStipend.Equals(newStipend))
            {
                MessageBox.Show("Stipend is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    var newStudent = context.Students.SingleOrDefault(Student => Student.Id_student == newStipend.Id_student);

                    if (newStudent != null)
                    {
                       
                        var result = context.Stipends.SingleOrDefault(Stipend => Stipend.Id_stipend == oldStipend.Id_stipend);
                        if (result != null)
                        {
                            try
                            {
                                result.Id_student = newStipend.Id_student;
                                result.Name = newStipend.Name;
                                result.Start_date = newStipend.Start_date;
                                result.End_date = newStipend.End_date;
                                
                                context.SaveChanges();
                                MessageBox.Show($"Stipend has been changed");
                                this.Close();
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show($"Error: {exp.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Can't find old Stipend");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("This student doens't exist.");
                    }

                }
            }
        }

        public void createVarialbeToEdit(int id)
        {
            idStipend = id;
            DatabaseModel.Stipend stipend = findStipend(id);

            if (stipend != null)
            {
                idStipend = stipend.Id_stipend;
                idStudentVariable = stipend.Id_student;
                NameVariable = stipend.Name;
                startDateVariable = stipend.Start_date;
                endDateVariable = stipend.End_date;
                

                StudentIDNew.Text = idStudentVariable.ToString();
                NameNew.Text = NameVariable.ToString();
                StartDateNew.SelectedDate = startDateVariable;
                EndDateNew.SelectedDate = endDateVariable;
            }
        }
        private DatabaseModel.Stipend findStipend(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Stipend stipendToFind = new DatabaseModel.Stipend();
                    stipendToFind = context.Stipends.FirstOrDefault(Stipend => Stipend.Id_stipend == id);
                    return stipendToFind;
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
