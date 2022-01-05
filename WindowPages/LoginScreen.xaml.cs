using System;
using System.Windows;
using System.Linq;

namespace DesktopApk.WindowPages
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtUsername.Text;
            var password = txtPassword.Password;

            using (var context = new DatabaseModel.SZRBDApplicationEntities())
            {
                var item = context.Logins.FirstOrDefault(l => l.Username == userName);
                
                if (item.Username == userName && item.Password == password)
                {
                    foreach(var items in context.Admins)
                    {
                        if (item.Id_login == items.Id_login)
                        {
                            MainWindow dashboard = new MainWindow();
                            dashboard.LoginID = item.Id_login;
                            dashboard.Show();
                            this.Close();
                            MessageBox.Show("Connected");
                        }
                        else
                        {
                            MessageBox.Show("You are not Admin");
                        }
                    }
                }else
                {
                    MessageBox.Show("Wrong Login or Password. Try Again.");
                }
                
            }

        }
    }
}
