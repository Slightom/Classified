namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class searchingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchModels",
                c => new
                    {
                        SearchModelID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Location = c.String(),
                        State = c.String(),
                        PriceMin = c.Double(nullable: false),
                        PriceMax = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SearchModelID);
            
            DropTable("dbo.UploadModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UploadModels",
                c => new
                    {
                        UploadModelID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UploadModelID);
            
            DropTable("dbo.SearchModels");
        }
    }
}
