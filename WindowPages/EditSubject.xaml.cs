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
    /// Interaction logic for EditSubject.xaml
    /// </summary>
    public partial class EditSubject : Window
    {
        private String subjectName { get; set; }
        private int subjectID { get; set; }
        public EditSubject()
        {
            InitializeComponent();
        }

        public void createVarialbeToEdit(int id)
        {
            subjectID = id;
            DatabaseModel.Subject subject = findSubject(id);

            if (subject != null)
            {
                subjectName = subject.Name;

                NameEdit.Text = subject.Name;
            }

        }

        private DatabaseModel.Subject findSubject(int id)
        {
            try
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    DatabaseModel.Subject subjectToFind = new DatabaseModel.Subject();
                    subjectToFind = context.Subjects.FirstOrDefault(Subject => Subject.Id_subject == subjectID);
                    return subjectToFind;
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
            DatabaseModel.Subject oldSubject = new DatabaseModel.Subject
            {
                Id_subject = subjectID,
                Name = subjectName,

            };

            DatabaseModel.Subject newSubject = new DatabaseModel.Subject
            {
                Id_subject = subjectID,
                Name = NameEdit.Text,
            };
            if (oldSubject.Equals(newSubject))
            {
                MessageBox.Show("Subject is the same");
                this.Close();
            }
            else
            {
                using (var context = new DatabaseModel.SZRBDApplicationEntities())
                {
                    var result = context.Subjects.SingleOrDefault(Subject => Subject.Id_subject == oldSubject.Id_subject);
                    if (result != null)
                    {
                        try
                        {
                            result.Name = newSubject.Name;

                            context.SaveChanges();
                            MessageBox.Show($"Student has been changed");
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
