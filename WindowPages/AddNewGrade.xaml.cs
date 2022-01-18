using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for AddNewGrade.xaml
    /// </summary>
    public partial class AddNewGrade : Window
    {
        private int gradeID { get; set; }
        private int lecturerID { get; set; }
        private int subjectID { get; set; }
        private int studentID { get; set; }
        private int GradeVariable { get; set; }


        public AddNewGrade()
        {
            InitializeComponent();
            GradeNew.SelectedValue = DataContext;
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    if (StudentIDNew.Text.Length == 0 || LecturerIDNew.Text.Length == 0 || SubjectIDNew.Text.Length == 0 || GradeDateNew.SelectedDate.HasValue == false || GradeNew.SelectedIndex == -1)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {

                        try
                        {
                            studentID = Int32.Parse(StudentIDNew.Text);
                            lecturerID = Int32.Parse(LecturerIDNew.Text);
                            subjectID = Int32.Parse(SubjectIDNew.Text);
                            GradeVariable = GradeNew.SelectedIndex;
                        }
                        catch (Exception exep)
                        {
                            MessageBox.Show($"Error : {exep.Message}");
                        }

                        var studentEx = context.Students.FirstOrDefault(Student => Student.Id_student == studentID);
                        var lecturerEx = context.Lecturers.FirstOrDefault(Lecturer => Lecturer.Id_lecturer == lecturerID);
                        var subjectEx = context.Subjects.FirstOrDefault(Subject => Subject.Id_subject == subjectID);

                        if (studentEx != null)
                        {
                            if (lecturerEx != null)
                            {
                                if(subjectEx != null)
                                {
                                    var grades = context.Set<DatabaseModel.Grade>();
                                    grades.Add(new DatabaseModel.Grade
                                    {
                                        Id_lecturer = lecturerID,
                                        Id_subject = subjectID,
                                        Id_student = studentID,
                                        Grade_date = (DateTime)GradeDateNew.SelectedDate,
                                        Grade1 = GradeVariable

                                    });
                                    context.SaveChanges();
                                    MessageBox.Show("You create new Grade");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Subject doesn't exist");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lecturer doesn't exist");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Student doens't exist");
                        }



                    }
                }
            }
            catch (DbEntityValidationException DBEntityException)
            {
                foreach (var eve in DBEntityException.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                MessageBox.Show("Cannot create Grade");
                this.Close();
            }
        }
    }
}
