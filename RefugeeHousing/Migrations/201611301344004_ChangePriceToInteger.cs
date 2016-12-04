namespace RefugeeHousing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePriceToInteger : DbMigration
    {
        public override void Up()
        {
            var constraintDrop = @"declare @table_name nvarchar(256);
                declare @col_name nvarchar(256);
                set @table_name = N'Listings';
                set @col_name = N'Price';

                declare @Command nvarchar(max) = ''

                select @Command = @Command + 'ALTER TABLE Listings DROP CONSTRAINT ' + d.name
                from sys.tables t
                    join
                    sys.default_constraints d
                        on d.parent_object_id = t.object_id
                    join
                    sys.columns c
                        on c.object_id = t.object_id
                        and c.column_id = d.parent_column_id
                where t.name = @table_name
                and c.name = @col_name;

                execute (@Command)";

            Sql(constraintDrop);
            AlterColumn("dbo.Listings", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            Sql("ALTER TABLE Listings ADD  DEFAULT 0 FOR Price");
        }
    }
}
