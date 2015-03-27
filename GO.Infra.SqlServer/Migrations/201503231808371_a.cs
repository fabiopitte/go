namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBrand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblCustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        CNPJ = c.String(maxLength: 14, unicode: false),
                        IE = c.String(maxLength: 10, unicode: false),
                        RazaoSocial = c.String(maxLength: 100, unicode: false),
                        Observacoes = c.String(maxLength: 255, unicode: false),
                        DDDTelefone = c.String(maxLength: 3, unicode: false),
                        Telefone = c.String(maxLength: 10, unicode: false),
                        DDDCelular = c.String(maxLength: 3, unicode: false),
                        Celular = c.String(maxLength: 10, unicode: false),
                        TipoPessoa = c.Byte(nullable: false),
                        Endereco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAddress", t => t.Endereco_Id)
                .Index(t => t.Endereco_Id);
            
            CreateTable(
                "dbo.tblAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(maxLength: 100, unicode: false),
                        Number = c.String(maxLength: 50, unicode: false),
                        District = c.String(maxLength: 50, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        Estate = c.String(maxLength: 50, unicode: false),
                        CEP = c.String(maxLength: 10, unicode: false),
                        Complement = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblProduct", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.tblSale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.tblProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        InsertDate = c.DateTime(),
                        Margin = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SupplierId = c.Int(),
                        BrandId = c.Int(),
                        Description = c.String(maxLength: 255, unicode: false),
                        Style = c.String(maxLength: 50, unicode: false),
                        Size = c.String(maxLength: 50, unicode: false),
                        Measure = c.String(maxLength: 50, unicode: false),
                        Color = c.String(maxLength: 50, unicode: false),
                        Model = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBrand", t => t.BrandId)
                .ForeignKey("dbo.tblCategory", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.tblSupplier", t => t.SupplierId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.tblPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblSupplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        CNPJ = c.String(maxLength: 14, unicode: false),
                        IE = c.String(maxLength: 10, unicode: false),
                        RazaoSocial = c.String(maxLength: 100, unicode: false),
                        Observacoes = c.String(maxLength: 255, unicode: false),
                        DDDTelefone = c.String(maxLength: 3, unicode: false),
                        Telefone = c.String(maxLength: 10, unicode: false),
                        DDDCelular = c.String(maxLength: 3, unicode: false),
                        Celular = c.String(maxLength: 10, unicode: false),
                        Endereco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAddress", t => t.Endereco_Id)
                .Index(t => t.Endereco_Id);
            
            CreateTable(
                "dbo.tblSale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCustomer", t => t.CustomerId)
                .ForeignKey("dbo.tblUSer", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.tblUSer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Login = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        DDDTelefone = c.String(maxLength: 3, unicode: false),
                        Telefone = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhotoProducts",
                c => new
                    {
                        Photo_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_Id, t.Product_Id })
                .ForeignKey("dbo.tblPhoto", t => t.Photo_Id, cascadeDelete: true)
                .ForeignKey("dbo.tblProduct", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Photo_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblItem", "SaleId", "dbo.tblSale");
            DropForeignKey("dbo.tblSale", "UserId", "dbo.tblUSer");
            DropForeignKey("dbo.tblSale", "CustomerId", "dbo.tblCustomer");
            DropForeignKey("dbo.tblItem", "ProductId", "dbo.tblProduct");
            DropForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier");
            DropForeignKey("dbo.tblSupplier", "Endereco_Id", "dbo.tblAddress");
            DropForeignKey("dbo.PhotoProducts", "Product_Id", "dbo.tblProduct");
            DropForeignKey("dbo.PhotoProducts", "Photo_Id", "dbo.tblPhoto");
            DropForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory");
            DropForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand");
            DropForeignKey("dbo.tblCustomer", "Endereco_Id", "dbo.tblAddress");
            DropIndex("dbo.PhotoProducts", new[] { "Product_Id" });
            DropIndex("dbo.PhotoProducts", new[] { "Photo_Id" });
            DropIndex("dbo.tblSale", new[] { "CustomerId" });
            DropIndex("dbo.tblSale", new[] { "UserId" });
            DropIndex("dbo.tblSupplier", new[] { "Endereco_Id" });
            DropIndex("dbo.tblProduct", new[] { "BrandId" });
            DropIndex("dbo.tblProduct", new[] { "SupplierId" });
            DropIndex("dbo.tblProduct", new[] { "CategoryId" });
            DropIndex("dbo.tblItem", new[] { "SaleId" });
            DropIndex("dbo.tblItem", new[] { "ProductId" });
            DropIndex("dbo.tblCustomer", new[] { "Endereco_Id" });
            DropTable("dbo.PhotoProducts");
            DropTable("dbo.tblUSer");
            DropTable("dbo.tblSale");
            DropTable("dbo.tblSupplier");
            DropTable("dbo.tblPhoto");
            DropTable("dbo.tblProduct");
            DropTable("dbo.tblItem");
            DropTable("dbo.tblAddress");
            DropTable("dbo.tblCustomer");
            DropTable("dbo.tblCategory");
            DropTable("dbo.tblBrand");
        }
    }
}
