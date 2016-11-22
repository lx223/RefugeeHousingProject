namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePropertyLocationRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Listings", "LocationId", "dbo.Locations");
            DropIndex("dbo.Listings", new[] { "LocationId" });
            AlterColumn("dbo.Listings", "LocationId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Listings", "LocationId");
            AddForeignKey("dbo.Listings", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "LocationId", "dbo.Locations");
            DropIndex("dbo.Listings", new[] { "LocationId" });
            AlterColumn("dbo.Listings", "LocationId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Listings", "LocationId");
            AddForeignKey("dbo.Listings", "LocationId", "dbo.Locations", "Id");
        }
    }
}
