namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segundo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblSupplier", "RazaoSocial", c => c.String(maxLength: 100));
            DropColumn("dbo.tblSupplier", "Response_Titulo");
            DropColumn("dbo.tblSupplier", "Response_Mensagem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblSupplier", "Response_Mensagem", c => c.String());
            AddColumn("dbo.tblSupplier", "Response_Titulo", c => c.String());
            AlterColumn("dbo.tblSupplier", "RazaoSocial", c => c.String());
        }
    }
}
