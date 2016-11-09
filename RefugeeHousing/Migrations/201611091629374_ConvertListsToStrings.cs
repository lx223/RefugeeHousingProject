namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertListsToStrings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "LanguagesSpoken", c => c.String());
            AddColumn("dbo.Listings", "Appliances", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "Appliances");
            DropColumn("dbo.Listings", "LanguagesSpoken");
        }
    }
}
