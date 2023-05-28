using System.Configuration;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

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

  
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSignUpWindow();
            

        }


        private bool SignIn(string username, string password)
        {
            string connectionString;
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("DBConnection.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("ConnectionString")!;

            bool isMatch = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [User] WHERE Login = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        isMatch = true;
                    }
                }
            }

            return isMatch;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (SignIn(username, password))
            {
                MessageBox.Show("Giriş başarılı!");
            }
            else
            {
                MessageBox.Show("Giriş başarısız!");
            }

        }
    }
}
