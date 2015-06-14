namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblSale", "Date", c => c.String(nullable: false));
            AlterColumn("dbo.tblSale", "DateDispatch", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblSale", "DateDispatch", c => c.DateTime());
            AlterColumn("dbo.tblSale", "Date", c => c.DateTime(nullable: false));
        }
    }
}
