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
    /// Interaction logic for AddNewSession.xaml
    /// </summary>
    public partial class AddNewSession : Window
    {
        private int studentID { get; set; }
        private int classID { get; set; }
        private int nrOfSession { get; set; }
        private int partOfSession { get; set; }
        private bool sessionPassed { get; set; }

        public AddNewSession()
        {
            InitializeComponent();
            SessionNrNew.SelectedValue = DataContext;
            PassNew.SelectedValue = DataContext;
            PartSessionNew.SelectedValue = DataContext;
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    if (StudentIDNew.Text.Length == 0 || ClassIDNew.Text.Length == 0 || StartDateNew.SelectedDate.HasValue == false || EndDateNew.SelectedDate.HasValue == false || SessionNrNew.SelectedIndex == -1 || PartSessionNew.SelectedIndex == -1 || PassNew.SelectedIndex == -1)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {
                        
                        try
                        {
                            studentID = Int32.Parse(StudentIDNew.Text);
                            classID = Int32.Parse(ClassIDNew.Text);
                            nrOfSession = SessionNrNew.SelectedIndex + 1;
                            partOfSession = PartSessionNew.SelectedIndex + 1;
                            sessionPassed = PassNew.SelectedIndex == 1 ? true : false;                          

                        }catch(Exception exep)
                        {
                            MessageBox.Show($"Error : {exep.Message}");
                        }

                        var studentEx = context.Students.FirstOrDefault(Student => Student.Id_student == studentID);
                        var classEx = context.Classes.FirstOrDefault(Class => Class.Id_class == classID);

                        if (studentEx != null)
                        {
                            if (classEx != null)
                            {
                               
                                var session = context.Set<DatabaseModel.Session>();
                                session.Add(new DatabaseModel.Session
                                {
                                    Id_student = studentID,
                                    Id_class = classID,
                                    Start_date = (DateTime)StartDateNew.SelectedDate,
                                    End_date = (DateTime)EndDateNew.SelectedDate,
                                    Nr_session = nrOfSession,
                                    Part_session = partOfSession,
                                    Pass = sessionPassed,
                                });
                                context.SaveChanges();
                                MessageBox.Show("You create new Session");
                                this.Close();
                            }else
                            {
                                MessageBox.Show("Class doesn't exist");
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

                MessageBox.Show("Cannot create Session");
                this.Close();
            }


        }
    }
    
}
