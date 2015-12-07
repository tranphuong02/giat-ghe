namespace CL.Transverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.P_Category", "ParentId");
            AddForeignKey("dbo.P_Category", "ParentId", "dbo.P_Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.P_Category", "ParentId", "dbo.P_Category");
            DropIndex("dbo.P_Category", new[] { "ParentId" });
        }
    }
}
