using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        //private SqlDataReader sqlDataReader;
        private SqlConnection conn;
        string connectionString;
        public SignUpWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("DBConection.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("UserConnection")!;
            conn = new SqlConnection(connectionString);
        }

        private bool SignUpUser(string userLogin, string password)
        {
            try
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE [Login] = @UserLogin AND [Password] = @Password";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@UserLogin", userLogin);
                checkCmd.Parameters.AddWithValue("@Password", password);
                int duplicateCount = (int)checkCmd.ExecuteScalar();

                if (duplicateCount > 0)
                {
                    MessageBox.Show("These credentials already exist. Please choose different credentials.");
                    return false;
                }
                string query = "INSERT INTO [User] ([Login], [Password]) VALUES (@UserLogin, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                cmd.Parameters.AddWithValue("@Password", password);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                conn.Close();
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = LoginSignUp.Text;
            string password = password1.Text;
            string pass2 = password2.Text;
            if (password == pass2)
            {
                if (string.IsNullOrEmpty(userLogin) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("You must enter a username and password.");
                    return;
                }

                bool isSignedUp = SignUpUser(userLogin, password);

                if (isSignedUp)
                {
                    MessageBox.Show("Your registration has been successfully created!");
                }
                else
                {
                    MessageBox.Show("An error occurred during registration. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("passwords are not the same");
            }

        }

    }
}
