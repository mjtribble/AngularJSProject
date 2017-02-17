namespace _491Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .Index(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "LocationID", "dbo.Locations");
            DropIndex("dbo.Items", new[] { "LocationID" });
            DropTable("dbo.Items");
        }
    }
}
