namespace VipeBus.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDriver : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .Index(t => t.BusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drivers", "BusId", "dbo.Buses");
            DropIndex("dbo.Drivers", new[] { "BusId" });
            DropTable("dbo.Drivers");
        }
    }
}
