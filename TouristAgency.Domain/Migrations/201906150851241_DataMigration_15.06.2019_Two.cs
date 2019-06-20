namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_15062019_Two : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "IsBloked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsBloked", c => c.Boolean());
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(maxLength: 256));
        }
    }
}
