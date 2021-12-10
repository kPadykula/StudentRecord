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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int loginID = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public int LoginID { set { loginID = value; }  }

        private void btn_Grades_Click(object sender, RoutedEventArgs e)
        {
            var panel = RightCenterGridParent;
            panel.Visibility = Visibility.Hidden;

            var mainPanel = RightCenterPanelGrades;
            mainPanel.Visibility = Visibility.Visible;

        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Students_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Lecturers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Subjects_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Branch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
