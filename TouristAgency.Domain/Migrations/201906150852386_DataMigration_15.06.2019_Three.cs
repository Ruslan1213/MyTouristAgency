namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_15062019_Three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsBloked", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsBloked");
        }
    }
}
