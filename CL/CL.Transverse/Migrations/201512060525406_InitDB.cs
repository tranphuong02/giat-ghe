namespace CL.Transverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.P_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(),
                        ResourceId = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.P_Resource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId);
            
            CreateTable(
                "dbo.P_Resource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alt = c.String(),
                        Url = c.String(),
                        Type = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.S_Configuration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.P_Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        Type = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.P_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        Content = c.String(),
                        ResourceId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.P_Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.P_Resource", t => t.ResourceId, cascadeDelete: false)
                .Index(t => t.ResourceId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.S_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        Email = c.String(),
                        Role = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.P_Post", "ResourceId", "dbo.P_Resource");
            DropForeignKey("dbo.P_Post", "CategoryId", "dbo.P_Category");
            DropForeignKey("dbo.P_Category", "ResourceId", "dbo.P_Resource");
            DropIndex("dbo.P_Post", new[] { "CategoryId" });
            DropIndex("dbo.P_Post", new[] { "ResourceId" });
            DropIndex("dbo.P_Category", new[] { "ResourceId" });
            DropTable("dbo.S_User");
            DropTable("dbo.P_Post");
            DropTable("dbo.P_Page");
            DropTable("dbo.S_Configuration");
            DropTable("dbo.P_Resource");
            DropTable("dbo.P_Category");
        }
    }
}
