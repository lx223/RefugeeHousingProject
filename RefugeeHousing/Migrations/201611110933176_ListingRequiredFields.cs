namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListingRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "LanguagesSpoken", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "LanguagesSpoken", c => c.String());
        }
    }
}
