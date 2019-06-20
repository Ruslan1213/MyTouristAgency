namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_120619_TWO : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.OrderStatus", "OrdersStatus", c => c.String(maxLength: 30));
            AlterColumn("dbo.TypesTours", "TypeTour", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TypesTours", "TypeTour", c => c.String());
            AlterColumn("dbo.OrderStatus", "OrdersStatus", c => c.String());
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false));
        }
    }
}
