namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class terceiro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        CNPJ = c.String(maxLength: 14),
                        IE = c.String(maxLength: 10),
                        RazaoSocial = c.String(maxLength: 100),
                        Observacoes = c.String(maxLength: 255),
                        DDDTelefone = c.String(maxLength: 3),
                        Telefone = c.String(maxLength: 10),
                        DDDCelular = c.String(maxLength: 3),
                        Celular = c.String(maxLength: 10),
                        Tipo = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblCustomer");
        }
    }
}
