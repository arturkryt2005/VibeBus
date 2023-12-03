namespace VipeBus.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Trips", new[] { "CompanyId" });
            DropColumn("dbo.Trips", "CompanyId");
            DropTable("dbo.Companies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trips", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "CompanyId");
            AddForeignKey("dbo.Trips", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
