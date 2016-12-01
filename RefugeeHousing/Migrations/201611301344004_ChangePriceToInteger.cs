namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePriceToInteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
