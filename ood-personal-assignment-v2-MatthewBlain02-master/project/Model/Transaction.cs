using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int TransactionTypeID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionTotal { get; set; }
        public int UserID { get; set; }

        public virtual List<TransactionItems> Items { get; set; }

        public Transaction()
        {

        }
        public override string ToString()
        {
            return string.Format($"{this.TransactionID} \t\t {this.TransactionDateTime} \t\t €{this.TransactionTotal}");
        }
    }
    public class TransactionType
    {
        public int TransactionTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public string TransactionTypeImg { get; set; }

        public override string ToString()
        {
            return this.TransactionTypeID.ToString() + " " + this.TransactionTypeName;
        }

    }
    public class TransactionItems
    {
        public int TransactionItemsID { get; set; }
        public virtual int TransactionID { get; set; }
        public int ProductID { get; set; }
        public int ItemQuantity { get; set; }
    }
}
