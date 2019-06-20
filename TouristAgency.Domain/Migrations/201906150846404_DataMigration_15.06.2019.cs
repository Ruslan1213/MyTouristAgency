namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_15062019 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(maxLength: 256));
        }
    }
}
