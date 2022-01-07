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
    /// Interaction logic for AddNewBranch.xaml
    /// </summary>
    public partial class AddNewBranch : Window
    {
        public AddNewBranch()
        {
            InitializeComponent();
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    if (branchName.Text.Length == 0)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {
                        var branchEx = context.Branch_type.FirstOrDefault(Branch => Branch.Name == branchName.Text);
                        if (branchEx != null)
                        {
                            MessageBox.Show("This branch type already exist!");
                        }
                        else
                        {
                            var branch = context.Set<DatabaseModel.Branch_type>();
                            branch.Add(new DatabaseModel.Branch_type
                            {
                                Name = branchName.Text
                            });

                            context.SaveChanges();
                            MessageBox.Show("You create new branch type");
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

                MessageBox.Show("Cannot create branch");
                this.Close();
            }
        }
    }
}
