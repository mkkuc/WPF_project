namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        Account_AccountID = c.Int(),
                        Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.Priority", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.Account_AccountID)
                .ForeignKey("dbo.Group", t => t.Group_GroupID)
                .Index(t => t.PriorityID)
                .Index(t => t.Account_AccountID)
                .Index(t => t.Group_GroupID);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        PriorityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PriorityID);
            
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
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupOwnerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
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
            DropForeignKey("dbo.Queue", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Issue", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.Account", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.Queue", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Issue", "Account_AccountID", "dbo.Account");
            DropForeignKey("dbo.Issue", "PriorityID", "dbo.Priority");
            DropIndex("dbo.Queue", new[] { "AccountID" });
            DropIndex("dbo.Queue", new[] { "GroupID" });
            DropIndex("dbo.Issue", new[] { "Group_GroupID" });
            DropIndex("dbo.Issue", new[] { "Account_AccountID" });
            DropIndex("dbo.Issue", new[] { "PriorityID" });
            DropIndex("dbo.Account", new[] { "Group_GroupID" });
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropTable("dbo.Role");
            DropTable("dbo.Group");
            DropTable("dbo.Queue");
            DropTable("dbo.Priority");
            DropTable("dbo.Issue");
            DropTable("dbo.Account");
        }
    }
}