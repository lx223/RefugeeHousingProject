namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListingDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Listings", "NumberOfBedrooms", c => c.Int(nullable: false));
            AddColumn("dbo.Listings", "Furnished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Listings", "Elevator", c => c.Boolean(nullable: false));
            DropColumn("dbo.Listings", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "Description", c => c.String());
            DropColumn("dbo.Listings", "Elevator");
            DropColumn("dbo.Listings", "Furnished");
            DropColumn("dbo.Listings", "NumberOfBedrooms");
            DropColumn("dbo.Listings", "Price");
        }
    }
}
