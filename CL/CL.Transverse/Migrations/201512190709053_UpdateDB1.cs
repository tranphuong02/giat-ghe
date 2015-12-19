namespace CL.Transverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.P_Category", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Category", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_Configuration", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_Configuration", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Page", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Page", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Post", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Post", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Resource", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Resource", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_User", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_User", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.S_User", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_User", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Resource", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Resource", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Post", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Post", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Page", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Page", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_Configuration", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.S_Configuration", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Category", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.P_Category", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
