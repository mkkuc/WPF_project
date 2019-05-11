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
                        RuleID = c.Int(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Group", t => t.Group_GroupID)
                .ForeignKey("dbo.Rule", t => t.RuleID, cascadeDelete: true)
                .Index(t => t.RuleID)
                .Index(t => t.Group_GroupID);
            
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
                "dbo.Quest",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PriorityID = c.Int(nullable: false),
                        Group_GroupID = c.Int(),
                        Account_AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.Priority", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.Group_GroupID)
                .ForeignKey("dbo.Account", t => t.Account_AccountID)
                .Index(t => t.PriorityID)
                .Index(t => t.Group_GroupID)
                .Index(t => t.Account_AccountID);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        PriorityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PriorityID);
            
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        RuleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RuleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quest", "Account_AccountID", "dbo.Account");
            DropForeignKey("dbo.Account", "RuleID", "dbo.Rule");
            DropForeignKey("dbo.Queue", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Quest", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.Quest", "PriorityID", "dbo.Priority");
            DropForeignKey("dbo.Account", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.Queue", "AccountID", "dbo.Account");
            DropIndex("dbo.Quest", new[] { "Account_AccountID" });
            DropIndex("dbo.Quest", new[] { "Group_GroupID" });
            DropIndex("dbo.Quest", new[] { "PriorityID" });
            DropIndex("dbo.Queue", new[] { "AccountID" });
            DropIndex("dbo.Queue", new[] { "GroupID" });
            DropIndex("dbo.Account", new[] { "Group_GroupID" });
            DropIndex("dbo.Account", new[] { "RuleID" });
            DropTable("dbo.Rule");
            DropTable("dbo.Priority");
            DropTable("dbo.Quest");
            DropTable("dbo.Group");
            DropTable("dbo.Queue");
            DropTable("dbo.Account");
        }
    }
}
