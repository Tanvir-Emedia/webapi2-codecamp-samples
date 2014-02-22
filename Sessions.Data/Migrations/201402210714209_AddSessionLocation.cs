namespace Sessions.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Location");
        }
    }
}
