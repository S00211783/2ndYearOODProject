namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductQuantity", c => c.Int(nullable: false));
        }
    }
}
