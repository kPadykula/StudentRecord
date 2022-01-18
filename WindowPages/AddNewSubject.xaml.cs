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
    /// Interaction logic for AddNewSubject.xaml
    /// </summary>
    public partial class AddNewSubject : Window
    {
        public AddNewSubject()
        {
            InitializeComponent();
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    if (subjectName.Text.Length == 0)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {
                        var subjectEx = context.Subjects.FirstOrDefault(Subject => Subject.Name == subjectName.Text);
                        if (subjectEx != null)
                        {
                            MessageBox.Show("This subject already exist!");
                        }
                        else
                        {
                            var subject = context.Set<DatabaseModel.Subject>();
                            subject.Add(new DatabaseModel.Subject
                            {
                                Name = subjectName.Text
                            });

                            context.SaveChanges();
                            MessageBox.Show("You create new subject");
                            this.Close();
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

                MessageBox.Show("Cannot create subject");
                this.Close();
            }
        }
    }
}
