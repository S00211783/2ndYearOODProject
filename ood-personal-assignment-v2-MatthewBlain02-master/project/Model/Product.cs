using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal ProductPrice { get; set; }

        public int cartQuantity { get; set; }
        public string ProductImg { get; set; }

        public Product(int productID, string productName, int categoryID, decimal productPrice, string productImg)
        {
            ProductID = productID;
            ProductName = productName;
            CategoryID = categoryID;
            ProductPrice = productPrice;
            ProductImg = productImg;
            cartQuantity = 0;
        }

        public Product()
        {
        }

        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return string.Format($"{this.ProductName} \t\t €{this.ProductPrice}");
        }
    }
}
