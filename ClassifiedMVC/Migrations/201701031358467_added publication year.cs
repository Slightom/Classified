namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpublicationyear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllAtributesModels", "Publication_Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllAtributesModels", "Publication_Year");
        }
    }
}
