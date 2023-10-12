namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedUserIDToTransactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "UserID", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Transactions", "UserID");
        }
    }
}
