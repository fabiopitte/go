namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblSupplier", "TipoPessoa", c => c.Byte(nullable: false));
            AddColumn("dbo.tblSupplier", "NomeContato", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.tblSupplier", "EmailContato", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.tblSupplier", "TelefoneContato", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSupplier", "TelefoneContato");
            DropColumn("dbo.tblSupplier", "EmailContato");
            DropColumn("dbo.tblSupplier", "NomeContato");
            DropColumn("dbo.tblSupplier", "TipoPessoa");
        }
    }
}
