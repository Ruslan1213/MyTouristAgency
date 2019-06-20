namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelsTypes",
                c => new
                    {
                        IdHotelsType = c.Int(nullable: false, identity: true),
                        HotelType = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdHotelsType);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        IdTour = c.Int(nullable: false, identity: true),
                        IdTypeTour = c.Int(),
                        IdHotelsType = c.Int(),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        StartNumberOfPeople = c.Int(nullable: false),
                        EndNumberOfPeople = c.Int(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdTour)
                .ForeignKey("dbo.HotelsTypes", t => t.IdHotelsType)
                .ForeignKey("dbo.TypesTours", t => t.IdTypeTour)
                .Index(t => t.IdTypeTour)
                .Index(t => t.IdHotelsType);
            
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        IdJourney = c.Int(nullable: false, identity: true),
                        IdTour = c.Int(nullable: false),
                        StartedDate = c.DateTime(nullable: false),
                        ExpirstionDate = c.DateTime(nullable: false),
                        StartedAmount = c.Int(nullable: false),
                        QuantitySold = c.Int(),
                        IsLastMinuteTrip = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdJourney)
                .ForeignKey("dbo.Tours", t => t.IdTour, cascadeDelete: true)
                .Index(t => t.IdTour);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        IdOrder = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(),
                        IdJourney = c.Int(),
                        IdOrderStatus = c.Int(),
                        Discount = c.Double(),
                        OrderStatus_IdOrder = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdOrder)
                .ForeignKey("dbo.Journeys", t => t.IdJourney)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatus_IdOrder)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.IdJourney)
                .Index(t => t.OrderStatus_IdOrder)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        IdOrder = c.Int(nullable: false, identity: true),
                        OrdersStatus = c.String(),
                    })
                .PrimaryKey(t => t.IdOrder);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdUser = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Mail = c.String(),
                        Password = c.String(),
                        IsBloked = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TypesTours",
                c => new
                    {
                        IdTypeTour = c.Int(nullable: false, identity: true),
                        TypeTour = c.String(),
                    })
                .PrimaryKey(t => t.IdTypeTour);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tours", "IdTypeTour", "dbo.TypesTours");
            DropForeignKey("dbo.Journeys", "IdTour", "dbo.Tours");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "OrderStatus_IdOrder", "dbo.OrderStatus");
            DropForeignKey("dbo.Orders", "IdJourney", "dbo.Journeys");
            DropForeignKey("dbo.Tours", "IdHotelsType", "dbo.HotelsTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "OrderStatus_IdOrder" });
            DropIndex("dbo.Orders", new[] { "IdJourney" });
            DropIndex("dbo.Journeys", new[] { "IdTour" });
            DropIndex("dbo.Tours", new[] { "IdHotelsType" });
            DropIndex("dbo.Tours", new[] { "IdTypeTour" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TypesTours");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.Journeys");
            DropTable("dbo.Tours");
            DropTable("dbo.HotelsTypes");
        }
    }
}
