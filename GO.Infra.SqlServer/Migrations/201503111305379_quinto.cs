namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quinto : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Photos", newName: "tblPhoto");
            DropForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier");
            DropIndex("dbo.tblProduct", new[] { "SupplierId" });
            AlterColumn("dbo.tblCategory", "Title", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblCustomer", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblCustomer", "Email", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblCustomer", "CNPJ", c => c.String(maxLength: 14, unicode: false));
            AlterColumn("dbo.tblCustomer", "IE", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblCustomer", "RazaoSocial", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblCustomer", "Observacoes", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.tblCustomer", "DDDTelefone", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.tblCustomer", "Telefone", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblCustomer", "DDDCelular", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.tblCustomer", "Celular", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblProduct", "Title", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblProduct", "Cost", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblProduct", "Margin", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblProduct", "SupplierId", c => c.Int());
            AlterColumn("dbo.tblProduct", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.tblProduct", "Tamanho", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblPhoto", "Title", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblSupplier", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.tblSupplier", "Email", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblSupplier", "CNPJ", c => c.String(maxLength: 14, unicode: false));
            AlterColumn("dbo.tblSupplier", "IE", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblSupplier", "RazaoSocial", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblSupplier", "Observacoes", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.tblSupplier", "DDDTelefone", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.tblSupplier", "Telefone", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.tblSupplier", "DDDCelular", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.tblSupplier", "Celular", c => c.String(maxLength: 10, unicode: false));
            CreateIndex("dbo.tblProduct", "SupplierId");
            AddForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier");
            DropIndex("dbo.tblProduct", new[] { "SupplierId" });
            AlterColumn("dbo.tblSupplier", "Celular", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblSupplier", "DDDCelular", c => c.String(maxLength: 3));
            AlterColumn("dbo.tblSupplier", "Telefone", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblSupplier", "DDDTelefone", c => c.String(maxLength: 3));
            AlterColumn("dbo.tblSupplier", "Observacoes", c => c.String(maxLength: 255));
            AlterColumn("dbo.tblSupplier", "RazaoSocial", c => c.String(maxLength: 100));
            AlterColumn("dbo.tblSupplier", "IE", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblSupplier", "CNPJ", c => c.String(maxLength: 14));
            AlterColumn("dbo.tblSupplier", "Email", c => c.String(maxLength: 100));
            AlterColumn("dbo.tblSupplier", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.tblPhoto", "Title", c => c.String());
            AlterColumn("dbo.tblProduct", "Tamanho", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblProduct", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.tblProduct", "SupplierId", c => c.Int(nullable: false));
            AlterColumn("dbo.tblProduct", "Margin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblProduct", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblProduct", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.tblCustomer", "Celular", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblCustomer", "DDDCelular", c => c.String(maxLength: 3));
            AlterColumn("dbo.tblCustomer", "Telefone", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblCustomer", "DDDTelefone", c => c.String(maxLength: 3));
            AlterColumn("dbo.tblCustomer", "Observacoes", c => c.String(maxLength: 255));
            AlterColumn("dbo.tblCustomer", "RazaoSocial", c => c.String(maxLength: 100));
            AlterColumn("dbo.tblCustomer", "IE", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblCustomer", "CNPJ", c => c.String(maxLength: 14));
            AlterColumn("dbo.tblCustomer", "Email", c => c.String(maxLength: 100));
            AlterColumn("dbo.tblCustomer", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.tblCategory", "Title", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.tblProduct", "SupplierId");
            AddForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.tblPhoto", newName: "Photos");
        }
    }
}
