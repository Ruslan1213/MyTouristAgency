namespace TouristAgency.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration_120619_TREE : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
        }
    }
}
