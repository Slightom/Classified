namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllAtributesModels",
                c => new
                    {
                        AllAtributesModelID = c.Int(nullable: false, identity: true),
                        fuel = c.String(),
                        Engine_power = c.Double(nullable: false),
                        Enginie_capacity = c.Double(nullable: false),
                        Body_type = c.String(),
                        Transmission = c.String(),
                        Country_of_origin = c.String(),
                        Mileage = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Color = c.String(),
                        Number_of_pages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AllAtributesModelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AllAtributesModels");
        }
    }
}
