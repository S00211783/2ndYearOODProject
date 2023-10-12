namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionItems", "TransactionID_TransactionID", "dbo.Transactions");
            DropIndex("dbo.TransactionItems", new[] { "TransactionID_TransactionID" });
            RenameColumn(table: "dbo.TransactionItems", name: "TransactionID_TransactionID", newName: "TransactionID");
            AlterColumn("dbo.TransactionItems", "TransactionID", c => c.Int(nullable: false));
            CreateIndex("dbo.TransactionItems", "TransactionID");
            AddForeignKey("dbo.TransactionItems", "TransactionID", "dbo.Transactions", "TransactionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionItems", "TransactionID", "dbo.Transactions");
            DropIndex("dbo.TransactionItems", new[] { "TransactionID" });
            AlterColumn("dbo.TransactionItems", "TransactionID", c => c.Int());
            RenameColumn(table: "dbo.TransactionItems", name: "TransactionID", newName: "TransactionID_TransactionID");
            CreateIndex("dbo.TransactionItems", "TransactionID_TransactionID");
            AddForeignKey("dbo.TransactionItems", "TransactionID_TransactionID", "dbo.Transactions", "TransactionID");
        }
    }
}
