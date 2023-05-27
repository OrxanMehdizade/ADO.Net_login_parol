using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ADO.Net_login_parol.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void OpenSignUpWindow()
        {
            SignUp signUpWindow = new SignUp();
            MainView mainView = new MainView();
            Window window = new Window
            {
                Title = "Sign Up",
                Content = signUpWindow,
                Width = 800,
                Height = 450
            };
            window.ShowDialog();

            Window mainWindow = Window.GetWindow(mainView);
            mainWindow.Close();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSignUpWindow();
        }

    }
}
