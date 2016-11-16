namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequireAllListingsToHaveAnOwner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Listings", new[] { "OwnerId" });
            AlterColumn("dbo.Listings", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Listings", "OwnerId");
            AddForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Listings", new[] { "OwnerId" });
            AlterColumn("dbo.Listings", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Listings", "OwnerId");
            AddForeignKey("dbo.Listings", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
