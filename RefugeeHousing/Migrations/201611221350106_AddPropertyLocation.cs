namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EnglishName = c.String(nullable: false),
                        GreekName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Listings", "LocationId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Listings", "LocationId");
            AddForeignKey("dbo.Listings", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "LocationId", "dbo.Locations");
            DropIndex("dbo.Listings", new[] { "LocationId" });
            DropColumn("dbo.Listings", "LocationId");
            DropTable("dbo.Locations");
        }
    }
}
