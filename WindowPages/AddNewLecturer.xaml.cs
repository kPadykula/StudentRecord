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
    /// Interaction logic for AddNewLecturer.xaml
    /// </summary>
    public partial class AddNewLecturer : Window
    {
        public AddNewLecturer()
        {
            InitializeComponent();
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rm = new Random();

                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var rand = rm.Next(100, 10000);
                    var login = context.Logins.FirstOrDefault(Login => Login.Username == (FirstName.Text + LastName.Text + rand));

                    while (login != default)
                    {
                        rand = rm.Next(100, 10000);
                        login = context.Logins.FirstOrDefault(Login => Login.Username == (FirstName.Text + LastName.Text + rand));
                    }
                    

                    if (FirstName.Text.Length == 0 || LastName.Text.Length == 0 || Address.Text.Length == 0)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {
                        var lecturer = context.Set<DatabaseModel.Lecturer>();
                        lecturer.Add(new DatabaseModel.Lecturer
                        {
                            First_name = FirstName.Text,
                            Last_name = LastName.Text,
                            Address = Address.Text,
                            Login = new DatabaseModel.Login
                            {
                                Username = FirstName.Text + LastName.Text + rand,
                                Password = "",
                                Created_date = DateTime.Now,
                                LastPassword_reset = null
                            }
                        });

                        context.SaveChanges();
                        MessageBox.Show("You create new Lecturer");
                        this.Close();
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

                MessageBox.Show("Cannot create Lecturer");
                this.Close();
            }
            
        }
    }
}
