namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        RoleID = c.Int(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Group", t => t.Group_GroupID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.Group_GroupID);
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PriorityID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        AssigneeID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.Account", t => t.AssigneeID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Priority", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.PriorityID)
                .Index(t => t.StatusID)
                .Index(t => t.AssigneeID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupOwnerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.Queue",
                c => new
                    {
                        QueueID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QueueID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        PriorityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PriorityID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Issue", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Issue", "PriorityID", "dbo.Priority");
            DropForeignKey("dbo.Issue", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Queue", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Queue", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Account", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.Issue", "AssigneeID", "dbo.Account");
            DropIndex("dbo.Queue", new[] { "AccountID" });
            DropIndex("dbo.Queue", new[] { "GroupID" });
            DropIndex("dbo.Issue", new[] { "GroupID" });
            DropIndex("dbo.Issue", new[] { "AssigneeID" });
            DropIndex("dbo.Issue", new[] { "StatusID" });
            DropIndex("dbo.Issue", new[] { "PriorityID" });
            DropIndex("dbo.Account", new[] { "Group_GroupID" });
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropTable("dbo.Role");
            DropTable("dbo.Status");
            DropTable("dbo.Priority");
            DropTable("dbo.Queue");
            DropTable("dbo.Group");
            DropTable("dbo.Issue");
            DropTable("dbo.Account");
        }
    }
}
