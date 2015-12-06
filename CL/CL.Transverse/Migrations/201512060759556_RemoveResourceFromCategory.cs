namespace CL.Transverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveResourceFromCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.P_Category", "ResourceId", "dbo.P_Resource");
            DropIndex("dbo.P_Category", new[] { "ResourceId" });
            DropColumn("dbo.P_Category", "ResourceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.P_Category", "ResourceId", c => c.Int(nullable: false));
            CreateIndex("dbo.P_Category", "ResourceId");
            AddForeignKey("dbo.P_Category", "ResourceId", "dbo.P_Resource", "Id", cascadeDelete: true);
        }
    }
}
