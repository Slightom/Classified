namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryPathadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classifieds", "CategoryPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classifieds", "CategoryPath");
        }
    }
}
