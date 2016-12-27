namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminMessages",
                c => new
                    {
                        AdminMessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdminMessageID);
            
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        AttributeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttributeID);
            
            CreateTable(
                "dbo.AttributeValues",
                c => new
                    {
                        AttributeValueID = c.Int(nullable: false, identity: true),
                        AttributeID = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttributeValueID)
                .ForeignKey("dbo.Attributes", t => t.AttributeID, cascadeDelete: true)
                .Index(t => t.AttributeID);
            
            CreateTable(
                "dbo.CategoryAttributes",
                c => new
                    {
                        CategoryAttributeID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryAttributeID)
                .ForeignKey("dbo.Attributes", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.AttributeID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryFatherID = c.Int(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryFatherID)
                .Index(t => t.CategoryFatherID);
            
            CreateTable(
                "dbo.Classifieds",
                c => new
                    {
                        ClassifiedID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        State = c.String(nullable: false),
                        Counter = c.Int(nullable: false),
                        Reported = c.String(),
                    })
                .PrimaryKey(t => t.ClassifiedID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ClassifiedAttributes",
                c => new
                    {
                        ClassifiedAttributeID = c.Int(nullable: false, identity: true),
                        ClassifiedID = c.Int(nullable: false),
                        AttributeID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ClassifiedAttributeID)
                .ForeignKey("dbo.Attributes", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("dbo.Classifieds", t => t.ClassifiedID, cascadeDelete: true)
                .Index(t => t.ClassifiedID)
                .Index(t => t.AttributeID);
            
            CreateTable(
                "dbo.ClassifiedLocations",
                c => new
                    {
                        ClassifiedLocationID = c.Int(nullable: false, identity: true),
                        ClassifiedID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassifiedLocationID)
                .ForeignKey("dbo.Classifieds", t => t.ClassifiedID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.ClassifiedID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.PCLs",
                c => new
                    {
                        PCLID = c.Int(nullable: false, identity: true),
                        PersonalizedCategoryID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PCLID)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("dbo.PersonalizedCategories", t => t.PersonalizedCategoryID, cascadeDelete: true)
                .Index(t => t.PersonalizedCategoryID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.PersonalizedCategories",
                c => new
                    {
                        PersonalizedCategoryID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        PriceMin = c.Double(nullable: false),
                        PriceMax = c.Double(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.PersonalizedCategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        SenderID = c.String(nullable: false, maxLength: 128),
                        ReceiverID = c.String(nullable: false, maxLength: 128),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.ReceiverID)
                .ForeignKey("dbo.Users", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiverID);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        ClassifiedID = c.Int(nullable: false),
                        MainPhoto = c.Boolean(nullable: false),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.PhotoID)
                .ForeignKey("dbo.Classifieds", t => t.ClassifiedID, cascadeDelete: true)
                .Index(t => t.ClassifiedID);
            
            CreateTable(
                "dbo.BannedWords",
                c => new
                    {
                        BannedWordID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BannedWordID);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Classifieds", "UserID", "dbo.Users");
            DropForeignKey("dbo.Photos", "ClassifiedID", "dbo.Classifieds");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.PersonalizedCategories", "UserID", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderID", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverID", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.PCLs", "PersonalizedCategoryID", "dbo.PersonalizedCategories");
            DropForeignKey("dbo.PersonalizedCategories", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.PCLs", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ClassifiedLocations", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ClassifiedLocations", "ClassifiedID", "dbo.Classifieds");
            DropForeignKey("dbo.ClassifiedAttributes", "ClassifiedID", "dbo.Classifieds");
            DropForeignKey("dbo.ClassifiedAttributes", "AttributeID", "dbo.Attributes");
            DropForeignKey("dbo.Classifieds", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "CategoryFatherID", "dbo.Categories");
            DropForeignKey("dbo.CategoryAttributes", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryAttributes", "AttributeID", "dbo.Attributes");
            DropForeignKey("dbo.AttributeValues", "AttributeID", "dbo.Attributes");
            DropIndex("dbo.Photos", new[] { "ClassifiedID" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.PersonalizedCategories", new[] { "UserID" });
            DropIndex("dbo.PersonalizedCategories", new[] { "CategoryID" });
            DropIndex("dbo.PCLs", new[] { "LocationID" });
            DropIndex("dbo.PCLs", new[] { "PersonalizedCategoryID" });
            DropIndex("dbo.ClassifiedLocations", new[] { "LocationID" });
            DropIndex("dbo.ClassifiedLocations", new[] { "ClassifiedID" });
            DropIndex("dbo.ClassifiedAttributes", new[] { "AttributeID" });
            DropIndex("dbo.ClassifiedAttributes", new[] { "ClassifiedID" });
            DropIndex("dbo.Classifieds", new[] { "CategoryID" });
            DropIndex("dbo.Classifieds", new[] { "UserID" });
            DropIndex("dbo.Categories", new[] { "CategoryFatherID" });
            DropIndex("dbo.CategoryAttributes", new[] { "AttributeID" });
            DropIndex("dbo.CategoryAttributes", new[] { "CategoryID" });
            DropIndex("dbo.AttributeValues", new[] { "AttributeID" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.BannedWords");
            DropTable("dbo.Photos");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.PersonalizedCategories");
            DropTable("dbo.PCLs");
            DropTable("dbo.Locations");
            DropTable("dbo.ClassifiedLocations");
            DropTable("dbo.ClassifiedAttributes");
            DropTable("dbo.Classifieds");
            DropTable("dbo.Categories");
            DropTable("dbo.CategoryAttributes");
            DropTable("dbo.AttributeValues");
            DropTable("dbo.Attributes");
            DropTable("dbo.AdminMessages");
        }
    }
}
