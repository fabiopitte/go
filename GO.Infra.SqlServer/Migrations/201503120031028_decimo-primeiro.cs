namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimoprimeiro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand");
            DropIndex("dbo.tblProduct", new[] { "BrandId" });
            AlterColumn("dbo.tblProduct", "BrandId", c => c.Int());
            CreateIndex("dbo.tblProduct", "BrandId");
            AddForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand");
            DropIndex("dbo.tblProduct", new[] { "BrandId" });
            AlterColumn("dbo.tblProduct", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProduct", "BrandId");
            AddForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand", "Id", cascadeDelete: true);
        }
    }
}
