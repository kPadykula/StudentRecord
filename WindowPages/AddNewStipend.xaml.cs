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
    /// Interaction logic for AddNewStipend.xaml
    /// </summary>
    public partial class AddNewStipend : Window
    {
        private int studentID { get; set; }
        public AddNewStipend()
        {
            InitializeComponent();
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {

                    if (StudentIDNew.Text.Length == 0 || NameNew.Text.Length == 0 || StartDateNew.SelectedDate.HasValue == false || EndDateNew.SelectedDate.HasValue == false)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {

                        try
                        {
                            studentID = Int32.Parse(StudentIDNew.Text);

                        }
                        catch (Exception exep)
                        {
                            MessageBox.Show($"Error : {exep.Message}");
                        }

                        var studentEx = context.Students.FirstOrDefault(Student => Student.Id_student == studentID);
                       

                        if (studentEx != null)
                        { 
                            var stipend = context.Set<DatabaseModel.Stipend>();
                            stipend.Add(new DatabaseModel.Stipend
                            {
                                Id_student = studentID,
                                Name = NameNew.Text,
                                Start_date = (DateTime)StartDateNew.SelectedDate,
                                End_date = (DateTime)EndDateNew.SelectedDate
                            });
                            context.SaveChanges();
                            MessageBox.Show("You create new Stipend");
                            this.Close();
                            
                           
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

                MessageBox.Show("Cannot create Stipend");
                this.Close();
            }


        }
    }
    
}
