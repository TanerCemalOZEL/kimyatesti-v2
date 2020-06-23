namespace kimyatesti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLogin");
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
        }
    }
}
