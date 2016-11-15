namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListingOwnerIdOnListing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "ListingOwnerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "ListingOwnerId");
        }
    }
}
