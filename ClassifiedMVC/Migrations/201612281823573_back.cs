namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class back : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Category_CategoryID" });
            DropColumn("dbo.Categories", "Category_CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Category_CategoryID", c => c.Int());
            CreateIndex("dbo.Categories", "Category_CategoryID");
            AddForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
