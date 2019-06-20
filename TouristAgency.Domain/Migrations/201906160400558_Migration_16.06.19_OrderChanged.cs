namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_160619_OrderChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "IdUser");
            DropColumn("dbo.Orders", "IdOrderStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IdOrderStatus", c => c.Int());
            AddColumn("dbo.Orders", "IdUser", c => c.Int());
        }
    }
}
