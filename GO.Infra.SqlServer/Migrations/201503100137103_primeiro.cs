namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primeiro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.tblSupplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        CNPJ = c.String(maxLength: 14),
                        IE = c.String(maxLength: 10),
                        RazaoSocial = c.String(),
                        Observacoes = c.String(maxLength: 255),
                        DDDTelefone = c.String(maxLength: 3),
                        Telefone = c.String(maxLength: 10),
                        DDDCelular = c.String(maxLength: 3),
                        Celular = c.String(maxLength: 10),
                        Response_Titulo = c.String(),
                        Response_Mensagem = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "CategoryId", "dbo.tblCategory");
            DropIndex("dbo.tblProduct", new[] { "CategoryId" });
            DropTable("dbo.tblSupplier");
            DropTable("dbo.tblProduct");
            DropTable("dbo.tblCategory");
        }
    }
}
