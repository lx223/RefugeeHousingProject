namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkMissingListingOwners : DbMigration
    {
        public override void Up()
        {
            // Ensure that all listings in the DB have some owner, so just default to the first user.
            // This is safe to do because we know that the database currently just contains test data.
            Sql("UPDATE Listings " +
                    "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers) " +
                    "WHERE OwnerId IS NULL; ");
        }
        
        public override void Down()
        {
            // Cannot reverse this migration because we don't know which listings originally had owners.
            // But this is safe, because the next Down migration will get rid of this column altogether.
        }
    }
}
