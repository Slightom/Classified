namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpaginationtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Pagination", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Pagination");
        }
    }
}
