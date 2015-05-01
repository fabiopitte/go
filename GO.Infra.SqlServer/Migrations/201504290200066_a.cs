namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhotoProducts", "Photo_Id", "dbo.tblPhoto");
            DropForeignKey("dbo.PhotoProducts", "Product_Id", "dbo.tblProduct");
            DropIndex("dbo.PhotoProducts", new[] { "Photo_Id" });
            DropIndex("dbo.PhotoProducts", new[] { "Product_Id" });
            AddColumn("dbo.tblPhoto", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblPhoto", "ProductId");
            AddForeignKey("dbo.tblPhoto", "ProductId", "dbo.tblProduct", "Id", cascadeDelete: true);
            DropTable("dbo.PhotoProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PhotoProducts",
                c => new
                    {
                        Photo_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_Id, t.Product_Id });
            
            DropForeignKey("dbo.tblPhoto", "ProductId", "dbo.tblProduct");
            DropIndex("dbo.tblPhoto", new[] { "ProductId" });
            DropColumn("dbo.tblPhoto", "ProductId");
            CreateIndex("dbo.PhotoProducts", "Product_Id");
            CreateIndex("dbo.PhotoProducts", "Photo_Id");
            AddForeignKey("dbo.PhotoProducts", "Product_Id", "dbo.tblProduct", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhotoProducts", "Photo_Id", "dbo.tblPhoto", "Id", cascadeDelete: true);
        }
    }
}
