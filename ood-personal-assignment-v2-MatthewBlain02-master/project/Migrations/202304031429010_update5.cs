﻿namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "TransactionTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "TransactionTotal", c => c.Int(nullable: false));
        }
    }
}
