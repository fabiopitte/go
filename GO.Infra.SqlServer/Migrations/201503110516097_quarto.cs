namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quarto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblCustomer", "TipoPessoa", c => c.Byte(nullable: false));
            AddColumn("dbo.tblProduct", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tblProduct", "Margin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tblProduct", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.tblProduct", "PhotoId", c => c.Int(nullable: false));
            AddColumn("dbo.tblProduct", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.tblProduct", "Tamanho", c => c.String(maxLength: 10));
            CreateIndex("dbo.tblProduct", "SupplierId");
            CreateIndex("dbo.tblProduct", "PhotoId");
            AddForeignKey("dbo.tblProduct", "PhotoId", "dbo.Photos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier", "Id", cascadeDelete: true);
            DropColumn("dbo.tblCustomer", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblCustomer", "Tipo", c => c.Byte(nullable: false));
            DropForeignKey("dbo.tblProduct", "SupplierId", "dbo.tblSupplier");
            DropForeignKey("dbo.tblProduct", "PhotoId", "dbo.Photos");
            DropIndex("dbo.tblProduct", new[] { "PhotoId" });
            DropIndex("dbo.tblProduct", new[] { "SupplierId" });
            DropColumn("dbo.tblProduct", "Tamanho");
            DropColumn("dbo.tblProduct", "Description");
            DropColumn("dbo.tblProduct", "PhotoId");
            DropColumn("dbo.tblProduct", "SupplierId");
            DropColumn("dbo.tblProduct", "Margin");
            DropColumn("dbo.tblProduct", "Cost");
            DropColumn("dbo.tblCustomer", "TipoPessoa");
            DropTable("dbo.Photos");
        }
    }
}
