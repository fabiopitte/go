namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oitavo : DbMigration
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
            
            AddColumn("dbo.tblProduct", "MarcaId", c => c.Int(nullable: false));
            AddColumn("dbo.tblProduct", "Estilo", c => c.String());
            AddColumn("dbo.tblProduct", "Medida", c => c.String());
            AddColumn("dbo.tblProduct", "Cor", c => c.String());
            AddColumn("dbo.tblProduct", "Modelo", c => c.String());
            AddColumn("dbo.tblProduct", "Brand_Id", c => c.Int());
            CreateIndex("dbo.tblProduct", "Brand_Id");
            AddForeignKey("dbo.tblProduct", "Brand_Id", "dbo.tblBrand", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "Brand_Id", "dbo.tblBrand");
            DropIndex("dbo.tblProduct", new[] { "Brand_Id" });
            DropColumn("dbo.tblProduct", "Brand_Id");
            DropColumn("dbo.tblProduct", "Modelo");
            DropColumn("dbo.tblProduct", "Cor");
            DropColumn("dbo.tblProduct", "Medida");
            DropColumn("dbo.tblProduct", "Estilo");
            DropColumn("dbo.tblProduct", "MarcaId");
            DropTable("dbo.tblBrand");
        }
    }
}
