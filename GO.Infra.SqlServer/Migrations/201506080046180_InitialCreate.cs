namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        CPF = c.String(maxLength: 14, unicode: false),
                        RG = c.String(maxLength: 100, unicode: false),
                        IE = c.String(maxLength: 10, unicode: false),
                        RazaoSocial = c.String(maxLength: 100, unicode: false),
                        Observacoes = c.String(maxLength: 255, unicode: false),
                        DDDTelefone = c.String(maxLength: 3, unicode: false),
                        Telefone = c.String(maxLength: 10, unicode: false),
                        DDDCelular = c.String(maxLength: 3, unicode: false),
                        Celular = c.String(maxLength: 10, unicode: false),
                        TipoPessoa = c.Byte(nullable: false),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAddress", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.tblAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(maxLength: 100, unicode: false),
                        Number = c.String(maxLength: 50, unicode: false),
                        District = c.String(maxLength: 50, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        State = c.String(maxLength: 50, unicode: false),
                        CEP = c.String(maxLength: 10, unicode: false),
                        Complement = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.String(),
                        Quantity = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductCode = c.String(),
                        ProductColor = c.String(),
                        ProductBrand = c.String(),
                        ProductTitle = c.String(),
                        ProductDispatched = c.Boolean(nullable: false),
                        DateDispatched = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblSale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.tblSale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Date = c.DateTime(nullable: false),
                        DateDispatch = c.DateTime(),
                        Observations = c.String(),
                        Payment = c.Byte(nullable: false),
                        PaymentType = c.Byte(nullable: false),
                        Times = c.Byte(nullable: false),
                        Amount = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Discount = c.String(),
                        Total = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCustomer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.tblUSer", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.UserId);
            
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
                "dbo.tblPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Url = c.String(nullable: false, maxLength: 500, unicode: false),
                        ProductId = c.Int(nullable: false),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblProduct", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tblProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 100, unicode: false),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Price = c.String(),
                        Cost = c.String(),
                        InsertDate = c.DateTime(),
                        Quantity = c.Int(),
                        StyleId = c.Int(),
                        CategoryId = c.Int(),
                        SupplierId = c.Int(),
                        BrandId = c.Int(),
                        Description = c.String(maxLength: 255, unicode: false),
                        Size = c.String(maxLength: 50, unicode: false),
                        Measure = c.String(maxLength: 50, unicode: false),
                        Color = c.String(maxLength: 50, unicode: false),
                        Model = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBrand", t => t.BrandId)
                .ForeignKey("dbo.tblCategory", t => t.CategoryId)
                .ForeignKey("dbo.tblStyle", t => t.StyleId)
                .ForeignKey("dbo.tblSupplier", t => t.SupplierId)
                .Index(t => t.StyleId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.tblStyle",
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
                        TipoPessoa = c.Byte(nullable: false),
                        NomeContato = c.String(maxLength: 100, unicode: false),
                        EmailContato = c.String(maxLength: 100, unicode: false),
                        TelefoneContato = c.String(maxLength: 100, unicode: false),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAddress", t => t.AddressId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier");
            DropForeignKey("dbo.tblSupplier", "AddressId", "dbo.tblAddress");
            DropForeignKey("dbo.tblProduct", "StyleId", "dbo.tblStyle");
            DropForeignKey("dbo.tblPhoto", "ProductId", "dbo.tblProduct");
            DropForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory");
            DropForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand");
            DropForeignKey("dbo.tblItem", "SaleId", "dbo.tblSale");
            DropForeignKey("dbo.tblSale", "UserId", "dbo.tblUSer");
            DropForeignKey("dbo.tblSale", "CustomerId", "dbo.tblCustomer");
            DropForeignKey("dbo.tblCustomer", "AddressId", "dbo.tblAddress");
            DropIndex("dbo.tblSupplier", new[] { "AddressId" });
            DropIndex("dbo.tblProduct", new[] { "BrandId" });
            DropIndex("dbo.tblProduct", new[] { "SupplierId" });
            DropIndex("dbo.tblProduct", new[] { "CategoryId" });
            DropIndex("dbo.tblProduct", new[] { "StyleId" });
            DropIndex("dbo.tblPhoto", new[] { "ProductId" });
            DropIndex("dbo.tblSale", new[] { "UserId" });
            DropIndex("dbo.tblSale", new[] { "CustomerId" });
            DropIndex("dbo.tblItem", new[] { "SaleId" });
            DropIndex("dbo.tblCustomer", new[] { "AddressId" });
            DropTable("dbo.tblSupplier");
            DropTable("dbo.tblStyle");
            DropTable("dbo.tblProduct");
            DropTable("dbo.tblPhoto");
            DropTable("dbo.tblUSer");
            DropTable("dbo.tblSale");
            DropTable("dbo.tblItem");
            DropTable("dbo.tblAddress");
            DropTable("dbo.tblCustomer");
            DropTable("dbo.tblCategory");
            DropTable("dbo.tblBrand");
        }
    }
}
