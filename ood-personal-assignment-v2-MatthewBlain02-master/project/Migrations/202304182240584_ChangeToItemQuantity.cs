namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToItemQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionItems", "ItemQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionItems", "ProductQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionItems", "ProductQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionItems", "ItemQuantity");
        }
    }
}
