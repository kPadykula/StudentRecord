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
    /// Interaction logic for AddNewClass.xaml
    /// </summary>
    public partial class AddNewClass : Window
    {
        public AddNewClass()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var itemBox = BranchTypeNew;
            List<DatabaseModel.Branch_type> list = new List<DatabaseModel.Branch_type>();

            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                foreach (var item in context.Branch_type)
                {
                    list.Add(item);
                }
                itemBox.ItemsSource = list;
                itemBox.SelectedValuePath = "Id_branch";
                itemBox.DisplayMemberPath = "Name";
            }
        }
        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            int selectedBranchTypeId = 0;

            try
            {
                selectedBranchTypeId = Int32.Parse(BranchTypeNew.SelectedValue.ToString());
            }
            catch (Exception ecp)
            {
                Console.WriteLine("Cannot Parse: " + ecp.Message);
            }
            
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    
                    if (ClassNameNew.Text.Length == 0 || CommencmentDateNew.SelectedDate.HasValue == false || EndDateNew.SelectedDate.HasValue == false || BranchTypeNew.SelectedIndex == -1)
                    {
                        MessageBox.Show("Complete all data");
                    }
                    else
                    {
                        var newClass = context.Set<DatabaseModel.Class>();
                        var branch = context.Branch_type.FirstOrDefault(Branch => Branch.Id_branch == selectedBranchTypeId);
                        newClass.Add(new DatabaseModel.Class
                        {
                            Name = ClassNameNew.Text,
                            Commencment_date = (DateTime)CommencmentDateNew.SelectedDate,
                            End_date = (DateTime)EndDateNew.SelectedDate,
                            Branch_type = branch,
                            Id_branch = branch.Id_branch

                        });

                        context.SaveChanges();
                        MessageBox.Show("You create new Class");
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

                MessageBox.Show("Cannot create Class");
                this.Close();
            }
            
        }
    }
}
