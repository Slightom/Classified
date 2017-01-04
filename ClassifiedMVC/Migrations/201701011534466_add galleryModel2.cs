namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgalleryModel2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryModels",
                c => new
                    {
                        GalleryModelID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.GalleryModelID);
            
            AddColumn("dbo.Photos", "GalleryModel_GalleryModelID", c => c.Int());
            CreateIndex("dbo.Photos", "GalleryModel_GalleryModelID");
            AddForeignKey("dbo.Photos", "GalleryModel_GalleryModelID", "dbo.GalleryModels", "GalleryModelID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "GalleryModel_GalleryModelID", "dbo.GalleryModels");
            DropIndex("dbo.Photos", new[] { "GalleryModel_GalleryModelID" });
            DropColumn("dbo.Photos", "GalleryModel_GalleryModelID");
            DropTable("dbo.GalleryModels");
        }
    }
}
