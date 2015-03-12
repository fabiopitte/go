namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nono : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblProduct", "Brand_Id", "dbo.tblBrand");
            DropIndex("dbo.tblProduct", new[] { "Brand_Id" });
            RenameColumn(table: "dbo.tblProduct", name: "Brand_Id", newName: "BrandId");
            AddColumn("dbo.tblProduct", "Style", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.tblProduct", "Size", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.tblProduct", "Measure", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.tblProduct", "Color", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.tblProduct", "Model", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblProduct", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProduct", "BrandId");
            AddForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand", "Id", cascadeDelete: true);
            DropColumn("dbo.tblProduct", "MarcaId");
            DropColumn("dbo.tblProduct", "Estilo");
            DropColumn("dbo.tblProduct", "Tamanho");
            DropColumn("dbo.tblProduct", "Medida");
            DropColumn("dbo.tblProduct", "Cor");
            DropColumn("dbo.tblProduct", "Modelo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblProduct", "Modelo", c => c.String());
            AddColumn("dbo.tblProduct", "Cor", c => c.String());
            AddColumn("dbo.tblProduct", "Medida", c => c.String());
            AddColumn("dbo.tblProduct", "Tamanho", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.tblProduct", "Estilo", c => c.String());
            AddColumn("dbo.tblProduct", "MarcaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tblProduct", "BrandId", "dbo.tblBrand");
            DropIndex("dbo.tblProduct", new[] { "BrandId" });
            AlterColumn("dbo.tblProduct", "BrandId", c => c.Int());
            DropColumn("dbo.tblProduct", "Model");
            DropColumn("dbo.tblProduct", "Color");
            DropColumn("dbo.tblProduct", "Measure");
            DropColumn("dbo.tblProduct", "Size");
            DropColumn("dbo.tblProduct", "Style");
            RenameColumn(table: "dbo.tblProduct", name: "BrandId", newName: "Brand_Id");
            CreateIndex("dbo.tblProduct", "Brand_Id");
            AddForeignKey("dbo.tblProduct", "Brand_Id", "dbo.tblBrand", "Id");
        }
    }
}
