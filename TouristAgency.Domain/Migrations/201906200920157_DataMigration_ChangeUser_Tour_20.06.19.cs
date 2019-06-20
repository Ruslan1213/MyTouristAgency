namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_ChangeUser_Tour_200619 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Journeys", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "IsBloked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "IsBloked", c => c.Boolean());
            AlterColumn("dbo.Journeys", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Tours", "IsDeleted", c => c.Boolean());
        }
    }
}
