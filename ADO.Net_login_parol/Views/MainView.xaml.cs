using System.Configuration;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System;

namespace ADO.Net_login_parol.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        private SqlConnection conn;
        string connectionString;
        public MainView()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("DBConection.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("UserConnection")!;
            conn = new SqlConnection(connectionString);
        }

        private bool AuthenticateUser(string userLogin, string password)
        {
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM [User] WHERE [Login]  = @UserLogin AND [Password] = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                conn.Close();
            }
        }


        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = txtUsername.Text;
            string password = txtPassword.Text;




            bool isAuthenticated = AuthenticateUser(userLogin, password);

            if (isAuthenticated)
            {
                MessageBox.Show("The data you are looking for is available in Data Base");
            }
            else
            {
                MessageBox.Show("The information you are looking for is not available in the database");
            }

        }
        private void OpenSignUpWindow()
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            Window.GetWindow(this).Close();
            signUpWindow.Show();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSignUpWindow();
        }
    }
}
