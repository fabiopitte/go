namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory");
            DropIndex("dbo.tblProduct", new[] { "CategoryId" });
            AddColumn("dbo.tblProduct", "Code", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblProduct", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblProduct", "Quantity", c => c.Int());
            AlterColumn("dbo.tblProduct", "CategoryId", c => c.Int());
            CreateIndex("dbo.tblProduct", "CategoryId");
            AddForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory", "Id");
            DropColumn("dbo.tblProduct", "Margin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblProduct", "Margin", c => c.Decimal(precision: 18, scale: 2));
            DropForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory");
            DropIndex("dbo.tblProduct", new[] { "CategoryId" });
            AlterColumn("dbo.tblProduct", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.tblProduct", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tblProduct", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tblProduct", "Code");
            CreateIndex("dbo.tblProduct", "CategoryId");
            AddForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory", "Id", cascadeDelete: true);
        }
    }
}
