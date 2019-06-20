namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_160619_OrderChanged_TourChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Discription", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CountOfJourneys", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CountOfJourneys");
            DropColumn("dbo.Tours", "Discription");
        }
    }
}
