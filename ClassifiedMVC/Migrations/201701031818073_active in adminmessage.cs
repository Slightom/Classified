namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeinadminmessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminMessages", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminMessages", "Active");
        }
    }
}
