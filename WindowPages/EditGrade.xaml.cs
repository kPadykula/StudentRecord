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
    /// Interaction logic for EditGrade.xaml
    /// </summary>
    public partial class EditGrade : Window
    {
        private int idGrade { get; set; }
        private int idLecturer { get; set; }
        private int idSubject { get; set; }
        private int idStudent { get; set; }
        private DateTime gradeDate { get; set; }
        private int gradeVariable { get; set; }
        private int idLecturerNew { get; set; }
        private int idSubjectNew { get; set; }
        private int idStudentNew { get; set; }
        private DateTime gradeDateNew { get; set; }
        private int gradeVariableNew { get; set; }


        public EditGrade()
        {
            InitializeComponent();
        }

        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Grade oldGrade = new DatabaseModel.Grade
            {
                Id_grade = idGrade,
                Id_lecturer = idLecturer,
                Id_subject = idSubject,
                Id_student = idStudent,
                Grade_date = gradeDate,
                Grade1 = gradeVariable
            };
            try
            {
                idStudentNew = Int32.Parse(StudentIDNew.Text);
                idLecturerNew = Int32.Parse(LecturerIDNew.Text);
                idSubjectNew = Int32.Parse(SubjectIDNew.Text);
                gradeVariableNew = GradeNew.SelectedIndex;

            }
            catch (Exception exep)
            {
                MessageBox.Show($"Error : {exep.Message}");
            }

            DatabaseModel.Grade newGrade = new DatabaseModel.Grade
            {
                Id_grade = idGrade,
                Id_lecturer = idLecturerNew,
                Id_subject = idSubjectNew,
                Id_student = idStudentNew,
                Grade_date = (DateTime)GradeDateNew.SelectedDate,
                Grade1 = gradeVariableNew
            };
            if (oldGrade.Equals(newGrade))
            {
                MessageBox.Show("Grade is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    var newStudent = context.Students.SingleOrDefault(Student => Student.Id_student == newGrade.Id_student);
                    var newLecturer = context.Lecturers.SingleOrDefault(Lecturer => Lecturer.Id_lecturer == newGrade.Id_lecturer);
                    var newSubject = context.Subjects.SingleOrDefault(Subject => Subject.Id_subject == newGrade.Id_subject);

                    if (newStudent != null)
                    {
                        if (newLecturer != null)
                        {
                            if (newSubject != null)
                            {
                                var result = context.Grades.SingleOrDefault(Grades => Grades.Id_grade == oldGrade.Id_grade);
                                if (result != null)
                                {
                                    try
                                    {
                                        result.Id_lecturer = newGrade.Id_lecturer;
                                        result.Id_subject = newGrade.Id_subject;
                                        result.Id_student = newGrade.Id_student;
                                        result.Grade_date = newGrade.Grade_date;
                                        result.Grade1 = newGrade.Grade1;

                                        context.SaveChanges();
                                        MessageBox.Show($"Grade has been changed");
                                        this.Close();
                                    }
                                    catch (Exception exp)
                                    {
                                        MessageBox.Show($"Error: {exp.Message}");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Can't find old session");
                                }
                            }
                            else
                            {
                                MessageBox.Show("This subject doesn't exist.");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("This Lecturer doesn't exist");
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
            idGrade = id;
            DatabaseModel.Grade grade = findGrade(id);

            if (grade != null)
            {
                idLecturer = grade.Id_lecturer;
                idSubject = grade.Id_subject;
                idStudent = grade.Id_student;
                gradeDate = grade.Grade_date;
                gradeVariable = grade.Grade1;

                LecturerIDNew.Text = idLecturer.ToString();
                SubjectIDNew.Text = idSubject.ToString();
                StudentIDNew.Text = idStudent.ToString();
                GradeDateNew.SelectedDate = gradeDate;
                GradeNew.SelectedIndex = gradeVariable;
            }
        }
        private DatabaseModel.Grade findGrade(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Grade gradeToFind = new DatabaseModel.Grade();
                    gradeToFind = context.Grades.FirstOrDefault(Session => Session.Id_grade == idGrade);
                    return gradeToFind;
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
