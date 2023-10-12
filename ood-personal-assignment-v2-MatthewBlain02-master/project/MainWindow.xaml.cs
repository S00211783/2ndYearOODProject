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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Declaring Variables
        private ListBox products;
        public User mUser = new User(0, "default", 123, false);
        private decimal total = 0.00m;

        //Establishing Connection To Database
        BlainPOSDB db = new BlainPOSDB();

        //Constructor With User Being Passed Through
        public MainWindow(User user)
        {
            InitializeComponent();
            mUser = user;
        }

        //Constuctor With User And Cart Being Passed Through (For When User Wants To Go Back From Subtotal To Main Without Cashing Off)
        public MainWindow(User user, List<Product> items)
        {
            InitializeComponent();
            mUser = user;
            foreach (Product p in items)
            {
                LbxCart.Items.Add(p);
                total = total + (p.ProductPrice * p.cartQuantity);
            }
            btnSubtotal.Content = $"Subtotal : €{total}";
        }

        //Checks What Number Was Selected And Inputs To TextBlock
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int inputNum = int.Parse(btn.Content.ToString());
            TbNumIn.Text = TbNumIn.Text + $"{inputNum}";
        }

        //Clears TextBlock
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            TbNumIn.Text = string.Empty;
        }

        //Sign Off Account (Security Check - Makes Sure ID is entered beforehand)
        private void btnSignOff_Click(object sender, RoutedEventArgs e)
        {
            int userInput = 0;
            int userId = mUser.Id;
            string idIncorrect = "Inncorrect Id Entered Please Try Again";
            if (TbNumIn.Text != string.Empty)
            {
                userInput = int.Parse(TbNumIn.Text.ToString());
                if (userId == userInput)
                {
                    Login log = new Login();
                    log.Show();
                    Close();
                }

                else
                {
                    MessageBox.Show(idIncorrect);
                    TbNumIn.Text = string.Empty;
                }

            }
            else
            {
                MessageBox.Show(idIncorrect);
                TbNumIn.Text = string.Empty;
            }
        }

        //Adds Categories and Products When Window Is Loaded 
        private void TCMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Categories
                        select c;
            foreach (var c in query)
            {
                TabItem tab = new TabItem() //Creates New Tab For Each Category
                {
                    Header = c.CategoryName,
                    Name = "TI" + c.CategoryID.ToString(),

                };


                TCMenu.Items.Add(tab);
                //Adding Products To Category Tabs
                var query2 = from p in db.Products
                             where p.CategoryID == c.CategoryID
                             select p;
                products = new ListBox();
                products.ItemTemplate = Resources["ProductsTemplate"] as DataTemplate;
                products.ItemsSource = query2.ToList();
                products.SelectionChanged += new SelectionChangedEventHandler(productSelected);
                tab.Content = products;



            }
        }

        //Adds Data To Header
        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime currentDate = DateTime.Today;
            TbDate.Text = TbDate.Text + currentDate.ToShortDateString();
            TbID.Text = TbID.Text + mUser.Id;
            TbName.Text = TbName.Text + mUser.Name;
        }
        // Adds a product to the cart when selected. If the product is already there it will just add one to quantity and change total.
        private void productSelected(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            Product product = listBox.SelectedItem as Product;
            int productCurrentQuantity = 0;
            foreach (Product p in LbxCart.Items)
            {
                if (p.ProductID == product.ProductID)
                {
                    productCurrentQuantity = p.cartQuantity;
                    LbxCart.Items.Remove(p);
                    break;
                }
            }
            product.cartQuantity = productCurrentQuantity + 1;
            total = total + product.ProductPrice;
            btnSubtotal.Content = $"Subtotal : €{total}";
            LbxCart.Items.Add(product);
        }

        // Clears the cart, cartQuantities, and total.
        public void btnVoidAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < LbxCart.Items.Count; i++)
            {
                Product p = LbxCart.Items[i] as Product;
                p.cartQuantity = 0;
            }

            LbxCart.Items.Clear();
            total = 0.00m;
            btnSubtotal.Content = $"Subtotal : €{total}";
        }

        // Changes the quantity of a product and updates the total.
        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            if (TbNumIn.Text == "" || LbxCart.SelectedItem == null)
            {
                MessageBox.Show("Invalid Selections Made");
            }
            else
            {
                int quantity = int.Parse(TbNumIn.Text);
                Product selected = LbxCart.SelectedItem as Product;
                total = total - (selected.ProductPrice * selected.cartQuantity);
                LbxCart.Items.Remove(LbxCart.SelectedItem);
                selected.cartQuantity = 1 * quantity;
                LbxCart.Items.Add(selected);
                total = total + (selected.ProductPrice * quantity);
                btnSubtotal.Content = $"Subtotal : €{total}";
                TbNumIn.Text = "";
            }
        }

        // Removes a selected product and updates the total.
        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            if (LbxCart.SelectedItem == null)
            {
                MessageBox.Show("Invalid Selections Made");
            }
            else
            {
                Product selected = LbxCart.SelectedItem as Product;
                total = total - (selected.ProductPrice * selected.cartQuantity);
                selected.cartQuantity = 0;
                btnSubtotal.Content = $"Subtotal : €{total}";
                LbxCart.Items.Remove(LbxCart.SelectedItem);
            }
        }

        // Opens the subtotal page and passes through user, total, and user.
        private void btnSubtotal_Click(object sender, RoutedEventArgs e)
        {
            if (total == 0)
            {
                MessageBox.Show("There Is Nothing In The Cart");
            }
            else
            {
                Subtotal sub = new Subtotal(LbxCart.Items, total, mUser);
                sub.Show();
                Close();
            }
        }

        // Opens the journals window to show the user's transactions for that day.
        private void btnJournels_Click(object sender, RoutedEventArgs e)
        {
            Journals journels = new Journals(mUser);
            journels.Show();
        }

        // Opens the admin window.
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin(mUser);
            admin.Show();
            Close();
        }

        // This method is called when the window is loaded.
        // It checks if the user is an administrator, and if so, creates and adds an "Admin" button to the command bar.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mUser.IsAdmin == true)
            {
                Button Admin = new Button();
                Admin.Click += new RoutedEventHandler(btnAdmin_Click);
                Admin.Content = "Admin";
                Admin.Height = 30;
                Thickness margin = Admin.Margin;
                margin.Left = 10;
                margin.Top = 10;
                margin.Right = 10;
                margin.Top = 10;
                Admin.Margin = margin;
                commandBar.Children.Add(Admin);
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if(TbNumIn.Text == "")
            {
             
            }
            else
            {
                int productNo = int.Parse(TbNumIn.Text);
                var query = from p in db.Products
                            where p.ProductID == productNo
                            select p;
                int productCurrentQuantity = 0;
                Product product = query.FirstOrDefault();
                if (product == null)
                {
                    MessageBox.Show("No Product With Given ID");
                }
                else
                {
                    foreach (Product p in LbxCart.Items)
                    {
                        if (p.ProductID == product.ProductID)
                        {
                            productCurrentQuantity = p.cartQuantity;
                            LbxCart.Items.Remove(p);
                            break;
                        }
                    }
                    product.cartQuantity = productCurrentQuantity + 1;
                    total = total + product.ProductPrice;
                    btnSubtotal.Content = $"Subtotal : €{total}";
                    LbxCart.Items.Add(product);
                    TbNumIn.Text = "";
                }
            }
            
        }
    }
}
