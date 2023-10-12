namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update030423 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "cartQuantity", c => c.Int());
            AddColumn("dbo.Products", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Discriminator");
            DropColumn("dbo.Products", "cartQuantity");
        }
    }
}
