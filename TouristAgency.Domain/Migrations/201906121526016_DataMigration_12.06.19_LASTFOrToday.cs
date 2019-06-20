namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_120619_LASTFOrToday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
