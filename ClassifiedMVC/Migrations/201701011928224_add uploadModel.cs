namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduploadModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "GalleryModel_GalleryModelID", "dbo.GalleryModels");
            DropIndex("dbo.Photos", new[] { "GalleryModel_GalleryModelID" });
            CreateTable(
                "dbo.UploadModels",
                c => new
                    {
                        UploadModelID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UploadModelID);
            
            DropColumn("dbo.Photos", "GalleryModel_GalleryModelID");
            DropTable("dbo.GalleryModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GalleryModels",
                c => new
                    {
                        GalleryModelID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.GalleryModelID);
            
            AddColumn("dbo.Photos", "GalleryModel_GalleryModelID", c => c.Int());
            DropTable("dbo.UploadModels");
            CreateIndex("dbo.Photos", "GalleryModel_GalleryModelID");
            AddForeignKey("dbo.Photos", "GalleryModel_GalleryModelID", "dbo.GalleryModels", "GalleryModelID");
        }
    }
}
