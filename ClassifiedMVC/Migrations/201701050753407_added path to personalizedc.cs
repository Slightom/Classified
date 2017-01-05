namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpathtopersonalizedc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalizedCategories", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalizedCategories", "Path");
        }
    }
}
