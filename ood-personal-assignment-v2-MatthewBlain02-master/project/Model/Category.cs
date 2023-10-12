using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;

namespace project
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual List<Product> Products { get; set; }
        public override string ToString()
        {
            return this.CategoryID.ToString() + " " + this.CategoryName;
        }
    }
}
