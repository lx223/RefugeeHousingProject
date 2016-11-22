namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMissingLocations : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Locations (Id,EnglishName,GreekName) VALUES" +
           "('ChIJ8UNwBh-9oRQR3Y1mdkU1Nic','Athens',N'Αθήνα')");

            Sql("UPDATE Listings " +
                "SET LocationId = (SELECT TOP 1 Id FROM Locations) " +
                "WHERE LocationId IS NULL; ");
        }
        
        public override void Down()
        {

        }
    }
}
