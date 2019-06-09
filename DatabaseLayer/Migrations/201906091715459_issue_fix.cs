namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issue_fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issue", "Role_RoleID", "dbo.Role");
            DropIndex("dbo.Issue", new[] { "Role_RoleID" });
            DropColumn("dbo.Issue", "Role_RoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issue", "Role_RoleID", c => c.Int());
            CreateIndex("dbo.Issue", "Role_RoleID");
            AddForeignKey("dbo.Issue", "Role_RoleID", "dbo.Role", "RoleID");
        }
    }
}
