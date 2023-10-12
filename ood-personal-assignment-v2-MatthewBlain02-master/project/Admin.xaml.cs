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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        BlainPOSDB db = new BlainPOSDB();
        User aUser = new User();
        public Admin(User mUser)
        {
            InitializeComponent();
            aUser = mUser;

        }
    
        //-----------------------------------Transactions Tab---------------------------------------
        private void Transaction_Loaded(object sender, RoutedEventArgs e)
        {
            //Transactions
            var query = from t in db.Transactions
                        select t;

            lbxTransactions.ItemsSource = query.ToList();
        }
        //When Listbox Selection Changes It Will Show The Selected Transactions Details As A Reciept
        private void lbxTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Transaction t = lbxTransactions.SelectedItem as Transaction;

            var query = from ti in db.TransactionItems
                        where ti.TransactionID == t.TransactionID
                        select ti;

            var getTransactionType = from type in db.TransactionTypes
                                     where t.TransactionTypeID == type.TransactionTypeID
                                     select type;

            TransactionType transactionType = getTransactionType.FirstOrDefault();

            tbReceipt.Text = "Blain's Store" +
                             "\n==================================" +
                             "\n";
            foreach (TransactionItems ti in query)
            {
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

        //-----------------------------------Products Tab---------------------------------------
        private void Product_Loaded(object sender, RoutedEventArgs e)
        {
            //Products
            var query = from p in db.Products
                        select p;

            lbxProducts.ItemsSource = query.ToList();
        
            //Fill ComboBox
            var query2 = from c in db.Categories
                         select c;
            List<Category> categories = query2.ToList();

            foreach (Category c in categories)
            {
                cbProductsCategory.Items.Add(c);
            }
        }
        private void lbxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbxProducts.SelectedItem == null)
            {

            }
            else
            {
                Product selected = lbxProducts.SelectedItem as Product;
                tbProductName.Text = selected.ProductName;
                tbProductPrice.Text = selected.ProductPrice.ToString();
                tbImgSrc.Text = selected.ProductImg;
                var query = from c in db.Categories
                            where c.CategoryID == selected.CategoryID
                            select c;
                foreach(Category ci in cbProductsCategory.Items)
                {
                    if(ci.CategoryName == query.First().CategoryName)
                    {
                        cbProductsCategory.SelectedItem = ci;
                    }
                }
            }
            

        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal price = decimal.Parse(tbProductPrice.Text);
            }
            catch
            {
                MessageBox.Show("Price Must Be A Decimal Value, Eg 12.99 or 12 for 12.00");
            }
            finally
            {
                Category selected = cbProductsCategory.SelectedItem as Category;
                decimal price = decimal.Parse(tbProductPrice.Text);
                Product product = new Product { ProductName = tbProductName.Text, ProductPrice = price, ProductImg = tbImgSrc.Text, CategoryID = selected.CategoryID };
                db.Products.Add(product);
                db.SaveChanges();
                MessageBox.Show("Product Has Been Added");
            }
            
        }

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (lbxProducts.SelectedItem == null)
            {

            }
            else
            {
                Category selected2 = cbProductsCategory.SelectedItem as Category;
                Product selected = lbxProducts.SelectedItem as Product;
                var query = from p in db.Products
                            where p.ProductID == selected.ProductID
                            select p;
                query.First().ProductName = tbProductName.Text;
                query.First().ProductPrice = decimal.Parse(tbProductPrice.Text);
                query.First().ProductImg = tbImgSrc.Text;
                query.First().CategoryID = selected2.CategoryID;
                db.SaveChanges();
                MessageBox.Show("Product has been updated");
                lbxProducts.ItemsSource = db.Products.ToList();
            }
        }

        private void btnDelProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selected = lbxProducts.SelectedItem as Product;
            db.Products.Remove(selected);
            db.SaveChanges();
            MessageBox.Show("Product Has Been Removed");
            lbxProducts.ItemsSource = db.Products.ToList();
        }
        private void tbxSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            string search = tbxSearchProduct.Text;
            var query = from p in db.Products
                        where p.ProductName.StartsWith(search)
                        select p;
            lbxProducts.ItemsSource = query.ToList();
        }
        //-----------------------------------Users Tab---------------------------------------
        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            //Users
            var query2 = from u in db.Users
                         select u;
            lbxUsers.ItemsSource = query2.ToList();
        }
        private void lbxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbxUsers.SelectedItem == null)
            {

            }
            else
            {
                User selected = lbxUsers.SelectedItem as User;
                tbName.Text = selected.Name;
                tbPassword.Text = Convert.ToString(selected.Password);
                if(selected.IsAdmin == true)
                {
                    cbiTrue.IsSelected = true;
                }
                else
                {
                    cbiFalse.IsSelected = true;
                }
            }
            

        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            bool isAdmin = false;
            int password = 0;
            if (cbiTrue.IsSelected == true)
            {
                isAdmin = true;
            }
            try
            {
                password = int.Parse(tbPassword.Text);
            }
            catch
            {
                MessageBox.Show("Password Can Only Be Numbers");
            }
            finally
            {
                User newUser = new User { Name = tbName.Text, Password = password, IsAdmin = isAdmin };
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("User Has Been Added");
                lbxUsers.ItemsSource = null;
                lbxUsers.ItemsSource = db.Users.ToList();
            }
            
        }
        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbxUsers.SelectedItem == null)
            {

            }
            else
            {
                User selected = lbxUsers.SelectedItem as User;
                var query = from u in db.Users
                            where u.Id == selected.Id
                            select u;
                int password = 0;
                bool isAdmin = false;
                if (cbiTrue.IsSelected == true)
                {
                    isAdmin = true;
                }
                try
                {
                    password = int.Parse(tbPassword.Text);
                }
                catch
                {
                    MessageBox.Show("Password Can Only Be Numbers");
                }
                query.First().Name = tbName.Text;
                query.First().Password = password;
                query.First().IsAdmin = isAdmin;

                if (aUser.Id == query.First().Id)
                    aUser = query.First();

                db.SaveChanges();
                MessageBox.Show("User has been updated");
                lbxUsers.ItemsSource = db.Users.ToList();
            }
        }
        private void btnDelUser_Click(object sender, RoutedEventArgs e)
        {
            User selected = lbxUsers.SelectedItem as User;
            if(aUser.Id == selected.Id)
            {
                MessageBox.Show("User Cannot Deleted Itself");
            }
            else
            {
                db.Users.Remove(selected);
                db.SaveChanges();
                MessageBox.Show("User Has Been Removed");
                lbxUsers.ItemsSource = db.Users.ToList();
            }
            
        }
        private void tbxSearchUser_KeyDown(object sender, KeyEventArgs e)
        {
            string search = tbxSearchUser.Text;
            var query = from u in db.Users
                        where u.Name.StartsWith(search)
                        select u;
            lbxUsers.ItemsSource = query.ToList();
        }
        //-----------------------------------Cateogories---------------------------------------
        private void Categories_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Categories
                        select c;

            lbxCategories.ItemsSource = query.ToList();
        }
        private void lbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbxCategories.SelectedItem == null)
            {

            }
            else
            {
                Category selected = lbxCategories.SelectedItem as Category;
                tbCategoryName.Text = selected.CategoryName;
            }
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category { CategoryName = tbCategoryName.Text };
            db.Categories.Add(category);
            db.SaveChanges();
            MessageBox.Show("Category Has Been Added");
            lbxCategories.ItemsSource = db.Categories.ToList();
            cbProductsCategory.ItemsSource = null;
            cbProductsCategory.Items.Clear();
            cbProductsCategory.ItemsSource = db.Categories.ToList();

        }

        private void btnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            Category category = lbxCategories.SelectedItem as Category;
            var query = from c in db.Categories
                        where c.CategoryID == category.CategoryID
                        select c;
            query.First().CategoryName= tbCategoryName.Text;
            db.SaveChanges();
            MessageBox.Show("Category Has Been Updated");
            lbxCategories.ItemsSource = db.Categories.ToList();
            cbProductsCategory.ItemsSource = null;
            cbProductsCategory.Items.Clear();
            cbProductsCategory.ItemsSource = db.Categories.ToList();
        }

        private void btnDelCategory_Click(object sender, RoutedEventArgs e)
        {
            Category category = lbxCategories.SelectedItem as Category;
            db.Categories.Remove(category);
            db.SaveChanges();
            MessageBox.Show("Category Has Been Deleted\nPlease Note All Products Has Been Deleted From That Category Also");
            lbxCategories.ItemsSource = db.Categories.ToList();
            cbProductsCategory.ItemsSource = null;
            cbProductsCategory.Items.Clear();
            cbProductsCategory.ItemsSource = db.Categories.ToList();
        }

        //-----------------------------------Transaction Types---------------------------------------
        private void TransactionType_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from t in db.TransactionTypes
                        select t;

            lbxTransactionTypes.ItemsSource = query.ToList();
        }
        private void lbxTransactionTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxCategories.SelectedItem == null)
            {

            }
            else
            {
                TransactionType selected = lbxTransactionTypes.SelectedItem as TransactionType;
                tbCategoryName.Text = selected.TransactionTypeName;
            }
        }

        private void btnAddTType_Click(object sender, RoutedEventArgs e)
        {
            TransactionType transactionType = new TransactionType { TransactionTypeName = tbTransactionTypeName.Text };
            db.TransactionTypes.Add(transactionType);
            db.SaveChanges();
            MessageBox.Show("Transaction Type Has Been Added");
            lbxTransactionTypes.ItemsSource = db.TransactionTypes.ToList();
        }

        private void btnSaveTType_Click(object sender, RoutedEventArgs e)
        {
            TransactionType transactionType = lbxTransactionTypes.SelectedItem as TransactionType;
            var query = from t in db.TransactionTypes
                        where t.TransactionTypeID == t.TransactionTypeID
                        select t;
            query.First().TransactionTypeName = tbTransactionTypeName.Text;
            db.SaveChanges();
            MessageBox.Show("Transaction Type Has Been Updated");
            lbxTransactionTypes.ItemsSource = db.TransactionTypes.ToList();
        }

        private void btnDelTType_Click(object sender, RoutedEventArgs e)
        {
            TransactionType transactionType = lbxTransactionTypes.SelectedItem as TransactionType;
            db.TransactionTypes.Remove(transactionType);
            db.SaveChanges();
            MessageBox.Show("Transaction Type Has Been Deleted");
            lbxTransactionTypes.ItemsSource = db.TransactionTypes.ToList();
        }

        //-----------------------------------Close---------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(aUser);
            main.Show();
            Close();
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxSearchUser.Clear();
            lbxUsers.ItemsSource = db.Users.ToList();
            tbxSearchProduct.Clear();
            lbxProducts.ItemsSource = db.Products.ToList();
        }
    }
}
