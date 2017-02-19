namespace Sample.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doughnuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FlavorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flavors", t => t.FlavorId, cascadeDelete: true)
                .Index(t => t.FlavorId);
            
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doughnuts", "FlavorId", "dbo.Flavors");
            DropIndex("dbo.Doughnuts", new[] { "FlavorId" });
            DropTable("dbo.Flavors");
            DropTable("dbo.Doughnuts");
        }
    }
}
