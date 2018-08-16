namespace EF_Practices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 10),
                        Address = c.String(),
                        Telephone = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CustomerName, name: "CustomerNameIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", "CustomerNameIndex");
            DropTable("dbo.Customers");
        }
    }
}
