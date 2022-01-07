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
    /// Interaction logic for EditSession.xaml
    /// </summary>
    public partial class EditSession : Window
    {
        private int idStudent { get; set; }
        private int idSession { get; set; }
        private int idClass { get; set; }
        private DateTime startDateVariable { get; set; }
        private DateTime? endDateVariable { get; set; }
        private int nrOfSession { get; set; }
        private int? partOfSession { get; set; }
        private bool? passedVariable { get; set; }

        private int studentIDNewSe { get; set; }
        private int classIDNewSe { get; set; }
        private int nrOfSessionNewSe { get; set; }

        private int? partOfSessionNewSe { get; set; }
        private bool? passedVariableNewSe { get; set; }

        public EditSession()
        {
            InitializeComponent();
        }

        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Session oldSession = new DatabaseModel.Session
            {
                Id_session = idSession,
                Id_class = idClass,
                Id_student = idStudent,
                Start_date = startDateVariable,
                End_date = endDateVariable,
                Nr_session = nrOfSession,
                Part_session = partOfSession,
                Pass = passedVariable
            };
            try
            {
                studentIDNewSe = Int32.Parse(StudentIDNew.Text);
                classIDNewSe = Int32.Parse(ClassIDNew.Text);
                nrOfSessionNewSe = SessionNrNew.SelectedIndex + 1;
                partOfSessionNewSe = PartSessionNew.SelectedIndex + 1;
                passedVariableNewSe = PassNew.SelectedIndex == 1 ? true : false;

            }
            catch (Exception exep)
            {
                MessageBox.Show($"Error : {exep.Message}");
            }

            DatabaseModel.Session newSession = new DatabaseModel.Session
            {

                Id_session = idSession,
                Id_class = classIDNewSe,
                Id_student = studentIDNewSe,
                Start_date = (DateTime)StartDateNew.SelectedDate,
                End_date = (DateTime)EndDateNew.SelectedDate,
                Nr_session = nrOfSessionNewSe,
                Part_session = partOfSessionNewSe,
                Pass = passedVariableNewSe
            };
            if (oldSession.Equals(newSession))
            {
                MessageBox.Show("Session is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    var newStudent = context.Students.SingleOrDefault(Student => Student.Id_student == newSession.Id_student);
                    var newClass = context.Classes.SingleOrDefault(Class => Class.Id_class == newSession.Id_class);

                    if(newStudent != null)
                    {
                        if(newClass != null)
                        {
                            var result = context.Sessions.SingleOrDefault(Session => Session.Id_session == oldSession.Id_session);
                            if (result != null)
                            {
                                try
                                {
                                    result.Id_student = newSession.Id_student;
                                    result.Id_class = newSession.Id_class;
                                    result.Start_date = newSession.Start_date;
                                    result.End_date = newSession.End_date;
                                    result.Nr_session = newSession.Nr_session;
                                    result.Part_session = newSession.Part_session;
                                    result.Pass = newSession.Pass;

                                    context.SaveChanges();
                                    MessageBox.Show($"Session has been changed");
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
                            MessageBox.Show("This Class doesn't exist");
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
            idSession = id;
            DatabaseModel.Session session = findSession(id);

            if (session != null)
            {
                idStudent = session.Id_student;
                idClass = session.Id_class;
                startDateVariable = session.Start_date;
                endDateVariable = session.End_date;
                nrOfSession = session.Nr_session;
                partOfSession = session.Part_session;
                passedVariable = session.Pass;


                StudentIDNew.Text = idStudent.ToString();
                ClassIDNew.Text = idClass.ToString();
                StartDateNew.SelectedDate = startDateVariable;
                EndDateNew.SelectedDate = endDateVariable;
                SessionNrNew.SelectedIndex = nrOfSession - 1;
                PartSessionNew.SelectedIndex = (int)partOfSession - 1;
                PassNew.SelectedIndex = passedVariable == true ? 1 : 0;

            }
        }
        private DatabaseModel.Session findSession(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Session sessionToFind = new DatabaseModel.Session();
                    sessionToFind = context.Sessions.FirstOrDefault(Session => Session.Id_session == idSession);
                    return sessionToFind;
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
