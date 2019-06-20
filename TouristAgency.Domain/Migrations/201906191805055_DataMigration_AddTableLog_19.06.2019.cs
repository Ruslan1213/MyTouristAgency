namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_AddTableLog_19062019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        IP = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Number = c.Int(nullable: false),
                        Field = c.String(),
                        Value = c.String(),
                        GUID = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
