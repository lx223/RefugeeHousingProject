namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListingOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Listings", "OwnerId");
            AddForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Listings", "ListingOwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "ListingOwnerId", c => c.String());
            DropForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Listings", new[] { "OwnerId" });
            DropColumn("dbo.Listings", "OwnerId");
        }
    }
}
