namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePriceToInteger : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Listings DROP CONSTRAINT DF__Listings__Price__1FCDBCEB");
            AlterColumn("dbo.Listings", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
