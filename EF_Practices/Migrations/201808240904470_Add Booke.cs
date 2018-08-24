namespace EF_Practices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Page = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Id", "dbo.Products");
            DropIndex("dbo.Books", new[] { "Id" });
            DropTable("dbo.Books");
        }
    }
}
