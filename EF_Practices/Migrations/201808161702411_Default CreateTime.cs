namespace EF_Practices.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DefaultCreateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CreateTime", c => c.DateTime(true, defaultValueSql: "GETDATE()"));
        }

        public override void Down()
        {
            AlterColumn("dbo.Products", "CreateTime", c => c.DateTime());
        }
    }
}