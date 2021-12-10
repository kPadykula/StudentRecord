using System.Windows;

namespace DesktopApk
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
                foreach (var item in context.Logins)
                {
                    if (item.Username == userName && item.Password == password)
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.LoginID = item.Id_login;
                        dashboard.Show();
                        this.Close();
                        MessageBox.Show("Connected");
                    }
                    else
                    {
                        MessageBox.Show("Wrong Login or Password. Try Again.");
                    }
                }
            }

        }
    }
}
