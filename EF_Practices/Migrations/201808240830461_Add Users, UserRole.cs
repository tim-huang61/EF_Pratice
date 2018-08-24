namespace EF_Practices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Account = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
        }
    }
}
