namespace VipeBus.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeparturePointId = c.Int(nullable: false),
                        DestinationPointId = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        DestinationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.DeparturePointId)
                .ForeignKey("dbo.Cities", t => t.DestinationPointId)
                .Index(t => t.DeparturePointId)
                .Index(t => t.DestinationPointId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BusId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BusId)
                .Index(t => t.CompanyId)
                .Index(t => t.UserId)
                .Index(t => t.RouteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "UserId", "dbo.Users");
            DropForeignKey("dbo.Trips", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Trips", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Trips", "BusId", "dbo.Buses");
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.Cities");
            DropForeignKey("dbo.Routes", "DeparturePointId", "dbo.Cities");
            DropIndex("dbo.Trips", new[] { "RouteId" });
            DropIndex("dbo.Trips", new[] { "UserId" });
            DropIndex("dbo.Trips", new[] { "CompanyId" });
            DropIndex("dbo.Trips", new[] { "BusId" });
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropIndex("dbo.Routes", new[] { "DeparturePointId" });
            DropTable("dbo.Trips");
            DropTable("dbo.Routes");
            DropTable("dbo.Companies");
            DropTable("dbo.Cities");
            DropTable("dbo.Buses");
        }
    }
}
