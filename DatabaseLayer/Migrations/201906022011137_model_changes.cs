namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_changes : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Account", name: "Group_GroupID", newName: "GroupID");
            RenameIndex(table: "dbo.Account", name: "IX_Group_GroupID", newName: "IX_GroupID");
            AddColumn("dbo.Issue", "Role_RoleID", c => c.Int());
            CreateIndex("dbo.Issue", "Role_RoleID");
            AddForeignKey("dbo.Issue", "Role_RoleID", "dbo.Role", "RoleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issue", "Role_RoleID", "dbo.Role");
            DropIndex("dbo.Issue", new[] { "Role_RoleID" });
            DropColumn("dbo.Issue", "Role_RoleID");
            RenameIndex(table: "dbo.Account", name: "IX_GroupID", newName: "IX_Group_GroupID");
            RenameColumn(table: "dbo.Account", name: "GroupID", newName: "Group_GroupID");
        }
    }
}
