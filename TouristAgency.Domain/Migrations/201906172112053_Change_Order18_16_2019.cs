namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Order18_16_2019 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderStatus_IdOrder", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "OrderStatus_IdOrder" });
            AddColumn("dbo.Orders", "OrderStatus_IdOrder1", c => c.Int());
            CreateIndex("dbo.Orders", "OrderStatus_IdOrder1");
            AddForeignKey("dbo.Orders", "OrderStatus_IdOrder1", "dbo.OrderStatus", "IdOrder");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStatus_IdOrder1", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "OrderStatus_IdOrder1" });
            DropColumn("dbo.Orders", "OrderStatus_IdOrder1");
            CreateIndex("dbo.Orders", "OrderStatus_IdOrder");
            AddForeignKey("dbo.Orders", "OrderStatus_IdOrder", "dbo.OrderStatus", "IdOrder");
        }
    }
}
