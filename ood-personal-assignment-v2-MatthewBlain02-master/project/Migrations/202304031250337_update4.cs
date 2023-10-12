namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        TransactionItemsID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        TransactionID_TransactionID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionItemsID)
                .ForeignKey("dbo.Transactions", t => t.TransactionID_TransactionID)
                .Index(t => t.TransactionID_TransactionID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        TransactionTypeID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        TransactionTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        TransactionTypeID = c.Int(nullable: false, identity: true),
                        TransactionTypeName = c.String(),
                        TransactionTypeImg = c.String(),
                    })
                .PrimaryKey(t => t.TransactionTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionItems", "TransactionID_TransactionID", "dbo.Transactions");
            DropIndex("dbo.TransactionItems", new[] { "TransactionID_TransactionID" });
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionItems");
        }
    }
}
