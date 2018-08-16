namespace EF_Practices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        PId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Category = c.String(maxLength: 25),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PId)
                .Index(t => t.ProductName, unique: true, name: "ProductName")
                .Index(t => t.Category, name: "CategoryIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", "CategoryIndex");
            DropIndex("dbo.Products", "ProductName");
            DropTable("dbo.Products");
        }
    }
}
