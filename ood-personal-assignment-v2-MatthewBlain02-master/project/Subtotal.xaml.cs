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
    /// Interaction logic for Subtotal.xaml
    /// </summary>
    public partial class Subtotal : Window
    {
        //Declaring Variables & Connecting To Database
        BlainPOSDB db = new BlainPOSDB();
        decimal total = 0.00m;
        User stUser = new User();
        
        //Contructor With Cart Items, Cart Total and User being passed through
        public Subtotal(ItemCollection items, decimal cartTotal, User user)
        {
            InitializeComponent();
            LbxCart.ItemsSource = items;
            total = cartTotal;
            btnSubtotal.Content = $"Subtotal : €{total}";
            stUser = user;
        }

        //Adds All The Transaction Types To Listbox
        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from t in db.TransactionTypes
                        select t;

            foreach(TransactionType item in query)
            {
                lbxTransactionType.Items.Add(item);
            }
        }
        
        //Checks What Number Was Selected And Inputs To TextBlock (Added '.' To Make It Easier To Parse As A Deciamal)
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            try
            {
                int input = int.Parse(btn.Content.ToString());
                TbNumIn.Text = TbNumIn.Text + $"{input}";
            }
            catch
            {
                
                string input = btn.Content.ToString();
                if (TbNumIn.Text.Contains("."))
                {

                }
                else
                {
                    TbNumIn.Text = TbNumIn.Text + input;
                }
            }
                

            
        }

        //Clears The Text Block or Goes Back To Main Window Depending On The Text Block Being Empty Or Not
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if(TbNumIn.Text != "")
            {
                TbNumIn.Text = "";
            }
            else
            {
                List<Product> cartItems = new List<Product>();
                foreach(Product p in LbxCart.Items)
                {
                    cartItems.Add(p);
                }
                MainWindow main = new MainWindow(stUser, cartItems);
                main.Show();
                Close();
            }
            
        }

        //Nothing Set Up With This Yet
        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {

        }

        //Submits Transaction Depending On What Type Is Selected And If The User Has Inputted The Correct Amount
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if(TbNumIn.Text == "")
            {
                MessageBox.Show("No Value Entered");
            }
            else
            {
                decimal valueInput = decimal.Parse(TbNumIn.Text);
                if((valueInput - total) < 0)
                {
                    MessageBox.Show("Not Enough Given");
                }
                else
                {
                    if(lbxTransactionType.SelectedItem is null)
                    {
                        MessageBox.Show("Please Select Payment Type");
                    }
                    else
                    {
                        Transaction transaction = new Transaction();
                        TransactionType transactionType = lbxTransactionType.SelectedItem as TransactionType;
                        transaction.UserID = stUser.Id;
                        transaction.TransactionTypeID = transactionType.TransactionTypeID;
                        transaction.TransactionDateTime = DateTime.Now;
                        transaction.TransactionTotal = total;
                        List<TransactionItems> transactionItems = new List<TransactionItems>();
                        foreach (Product p in LbxCart.Items)
                        {
                            TransactionItems tp = new TransactionItems();
                            tp.TransactionID = transaction.TransactionID;
                            tp.ProductID = p.ProductID;
                            tp.ItemQuantity = p.cartQuantity;
                            transactionItems.Add(tp);
                        }
                        db.Transactions.Add(transaction);
                        foreach (TransactionItems t in transactionItems)
                        {
                            db.TransactionItems.Add(t);
                        }
                        db.SaveChanges();
                        MessageBox.Show($"Transaction {transaction.TransactionID} Has Been Successful\nTotal : €{total} Paid By : {transactionType.TransactionTypeName}\nChange To Be Given : €{valueInput - total}");
                        MainWindow main = new MainWindow(stUser);
                        main.Show();
                        Close();
                    }
                    
                }

            }
            
        }
    }
}
