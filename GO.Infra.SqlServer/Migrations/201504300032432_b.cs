namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblPhoto", "ProductId", "dbo.tblProduct");
            DropIndex("dbo.tblPhoto", new[] { "ProductId" });
            CreateTable(
                "dbo.ProductPhotoes",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Photo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Photo_Id })
                .ForeignKey("dbo.tblProduct", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.tblPhoto", t => t.Photo_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Photo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPhotoes", "Photo_Id", "dbo.tblPhoto");
            DropForeignKey("dbo.ProductPhotoes", "Product_Id", "dbo.tblProduct");
            DropIndex("dbo.ProductPhotoes", new[] { "Photo_Id" });
            DropIndex("dbo.ProductPhotoes", new[] { "Product_Id" });
            DropTable("dbo.ProductPhotoes");
            CreateIndex("dbo.tblPhoto", "ProductId");
            AddForeignKey("dbo.tblPhoto", "ProductId", "dbo.tblProduct", "Id", cascadeDelete: true);
        }
    }
}
