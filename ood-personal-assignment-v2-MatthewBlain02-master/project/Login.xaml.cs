using DocumentFormat.OpenXml.Spreadsheet;
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

namespace project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        // Creates a static instance of the BlainPOSDB class to access the database
        static BlainPOSDB db = new BlainPOSDB();

        // Constructor for the Login window
        public Login()
        {
            InitializeComponent();
        }

        // Handler for the Cancel button click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Handler for the Login button click event
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Retrieves a list of all users from the database
            List<User> users = db.Users.ToList();

            // Gets the user ID and password entered by the user from the corresponding textboxes

            int inputId = int.Parse(tbxID.Text);
            int inputPw = int.Parse(pwbPW.Password);



            // Error message to display if login fails
            string errorMessage = "Sorry Incorrect Id / Pin Entered";

            // LINQ query to check if the user ID and password entered match any records in the database
            var checklogin = from user in users
                                where inputId == user.Id && inputPw == user.Password
                                select user;

            // Gets the first user record returned by the query (if any)
            User u = checklogin.FirstOrDefault();

            // If the user record is found, open the MainWindow and pass the user object as a parameter
            if (u != null)
            {
                MainWindow main = new MainWindow(u);
                main.Show();
                Close();
            }
            // If the user record is not found, display an error message and clear the textboxes
            else
            {
                MessageBox.Show(errorMessage);
                tbxID.Text = String.Empty;
                pwbPW.Password = null;
            }
            
        }
    }
}
