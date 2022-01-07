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
    /// Interaction logic for EditBranch.xaml
    /// </summary>
    public partial class EditBranch : Window
    {
        private String branchName { get; set; }
        private int branchID { get; set; }
        public EditBranch()
        {
            InitializeComponent();
        }

        public void createVarialbeToEdit(int id)
        {
            branchID = id;
            DatabaseModel.Branch_type branch = findBranch(id);

            if (branch != null)
            {
                branchName = branch.Name;

                NameEdit.Text = branch.Name;
            }

        }

        private DatabaseModel.Branch_type findBranch(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Branch_type branchToFind = new DatabaseModel.Branch_type();
                    branchToFind = context.Branch_type.FirstOrDefault(Branch => Branch.Id_branch == branchID);
                    return branchToFind;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }
        private void SubmitButtonEdit(object sender, RoutedEventArgs e)
        {
            DatabaseModel.Branch_type oldBranch = new DatabaseModel.Branch_type
            {
                Id_branch = branchID,
                Name = branchName,

            };

            DatabaseModel.Branch_type newBranch = new DatabaseModel.Branch_type
            {
                Id_branch = branchID,
                Name = NameEdit.Text,
            };
            if (oldBranch.Equals(newBranch))
            {
                MessageBox.Show("Branch type is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var result = context.Branch_type.SingleOrDefault(Branch => Branch.Id_branch == oldBranch.Id_branch);
                    if (result != null)
                    {
                        try
                        {
                            result.Name = newBranch.Name;

                            context.SaveChanges();
                            MessageBox.Show($"Branch has been changed");
                            this.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show($"Error: {exp.Message}");
                        }
                    }
                }
            }
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
