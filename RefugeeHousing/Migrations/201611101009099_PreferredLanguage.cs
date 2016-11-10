namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreferredLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PreferredLanguage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PreferredLanguage");
        }
    }
}
