namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "LocationName", c => c.String(nullable: false));
            DropColumn("dbo.Locations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Locations", "LocationName");
        }
    }
}
