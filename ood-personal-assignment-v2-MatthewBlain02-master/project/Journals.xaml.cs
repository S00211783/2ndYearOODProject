using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for Journals.xaml
    /// </summary>
    public partial class Journals : Window
    {
        //Declaring Internal Variables and Connection To Database
        BlainPOSDB db = new BlainPOSDB();
        User jUser = new User();

        //Constructor Taking In User
        public Journals(User user)
        {
            InitializeComponent();
            jUser = user;
        }

        //When Window Loads All The Users Transactions For That Day Comes Up
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Retrieve transactions for the current user and today's date
            var query = from t in db.Transactions
                        where t.UserID == jUser.Id && DbFunctions.TruncateTime(t.TransactionDateTime) == DbFunctions.TruncateTime(DateTime.Today)
                        select t;

            //Bind the retrieved transactions to the listbox
            lbxTransactions.ItemsSource = query.ToList();
        }

        //When Listbox Selection Changes It Will Show The Selected Transactions Details As A Receipt
        private void lbxTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Retrieve the selected transaction
            Transaction t = lbxTransactions.SelectedItem as Transaction;

            //Retrieve the transaction items for the selected transaction
            var query = from ti in db.TransactionItems
                        where ti.TransactionID == t.TransactionID
                        select ti;

            //Retrieve the transaction type for the selected transaction
            var getTransactionType = from type in db.TransactionTypes
                                     where t.TransactionTypeID == type.TransactionTypeID
                                     select type;
            TransactionType transactionType = getTransactionType.FirstOrDefault();

            //Display the receipt details
            tbReceipt.Text = "Blain's Store" +
                             "\n==================================" +
                             "\n";
            foreach (TransactionItems ti in query)
            {
                //Retrieve the product information for each transaction item
                var getProduct = from p in db.Products
                                 where ti.ProductID == p.ProductID
                                 select p;
                Product product = getProduct.FirstOrDefault();
                tbReceipt.Text = tbReceipt.Text + $"{product.ProductName}\t{ti.ItemQuantity}\n";
            }

            tbReceipt.Text = tbReceipt.Text + $"Total : €{t.TransactionTotal}" +
                                              $"\nPaid By : {transactionType.TransactionTypeName}\t Date:{t.TransactionDateTime.ToShortDateString()}" +
                                              $"\n==================================" +
                                              $"\nThank You For Shopping With Us";
        }

        //Close the window when the Close button is clicked
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
