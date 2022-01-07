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
    /// Interaction logic for EditClass.xaml
    /// </summary>
    public partial class EditClass : Window
    {
        private int idClass { get; set; }
        private int idBranch { get; set; }
        private String NameN { get; set; }
        private DateTime CommencmentDate { get; set; }
        private DateTime? EndDate { get; set; }
        private DatabaseModel.Branch_type branchType { get; set; }
        public EditClass()
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
     
        public void createVarialbeToEdit(int id)
        {
            idClass = id;
            DatabaseModel.Class classe = findClass(id);

            if (classe != null)
            {
                NameN = classe.Name;
                CommencmentDate = classe.Commencment_date;
                EndDate = classe.End_date;
                idBranch = classe.Id_branch;
                
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    branchType = context.Branch_type.FirstOrDefault(Branch => Branch.Id_branch == classe.Id_branch);
                }
                
                ClassNameNew.Text = NameN;
                CommencmentDateNew.SelectedDate = CommencmentDate;
                EndDateNew.SelectedDate = EndDate;
                BranchTypeNew.SelectedValuePath = "Id_branch";
                BranchTypeNew.SelectedValue = branchType.Id_branch;
            }

        }

        private DatabaseModel.Class findClass(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Class classToFind = new DatabaseModel.Class();
                    classToFind = context.Classes.FirstOrDefault(Class => Class.Id_class == idClass);
                    return classToFind;
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
            DatabaseModel.Class oldClass = new DatabaseModel.Class
            {
                Id_class = idClass,
                Name = NameN,
                Commencment_date = CommencmentDate,
                End_date = EndDate,
                Id_branch = idBranch
            };

            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                branchType = context.Branch_type.FirstOrDefault(Branch => Branch.Id_branch == (int)BranchTypeNew.SelectedValue);
            }

            DatabaseModel.Class newClass = new DatabaseModel.Class
            {
                Id_class = idClass,
                Name = ClassNameNew.Text,
                Commencment_date = (DateTime)CommencmentDateNew.SelectedDate,
                End_date = (DateTime)EndDateNew.SelectedDate,
                Id_branch = branchType.Id_branch,
            };
            if (oldClass.Equals(newClass))
            {
                MessageBox.Show("Class is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var result = context.Classes.SingleOrDefault(Class => Class.Id_class == oldClass.Id_class);
                    if (result != null)
                    {
                        try
                        {
                            result.Name = newClass.Name;
                            result.Commencment_date = newClass.Commencment_date;
                            result.End_date = newClass.End_date;
                            result.Id_branch = newClass.Id_branch;

                            context.SaveChanges();
                            MessageBox.Show($"Class has been changed");
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
    }
}
