namespace VipeBus.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeparturePointId = c.Int(nullable: false),
                        DestinationPointId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        DestinationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.DeparturePointId)
                .ForeignKey("dbo.Cities", t => t.DestinationPointId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .Index(t => t.DeparturePointId)
                .Index(t => t.DestinationPointId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BusId)
                .Index(t => t.UserId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "UserId", "dbo.Users");
            DropForeignKey("dbo.Trips", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Trips", "BusId", "dbo.Buses");
            DropForeignKey("dbo.Routes", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.Cities");
            DropForeignKey("dbo.Routes", "DeparturePointId", "dbo.Cities");
            DropForeignKey("dbo.Drivers", "BusId", "dbo.Buses");
            DropIndex("dbo.Trips", new[] { "RouteId" });
            DropIndex("dbo.Trips", new[] { "UserId" });
            DropIndex("dbo.Trips", new[] { "BusId" });
            DropIndex("dbo.Routes", new[] { "DriverId" });
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropIndex("dbo.Routes", new[] { "DeparturePointId" });
            DropIndex("dbo.Drivers", new[] { "BusId" });
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
            DropTable("dbo.Routes");
            DropTable("dbo.Drivers");
            DropTable("dbo.Cities");
            DropTable("dbo.Buses");
        }
    }
}
