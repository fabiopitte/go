namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblItem", "Price", c => c.String());
            AlterColumn("dbo.tblSale", "Amount", c => c.String(nullable: false));
            AlterColumn("dbo.tblSale", "Discount", c => c.String());
            AlterColumn("dbo.tblSale", "Total", c => c.String(nullable: false));
            AlterColumn("dbo.tblProduct", "Price", c => c.String());
            AlterColumn("dbo.tblProduct", "Cost", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblProduct", "Cost", c => c.Double(nullable: false));
            AlterColumn("dbo.tblProduct", "Price", c => c.Double());
            AlterColumn("dbo.tblSale", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.tblSale", "Discount", c => c.Double());
            AlterColumn("dbo.tblSale", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.tblItem", "Price", c => c.Double(nullable: false));
        }
    }
}
