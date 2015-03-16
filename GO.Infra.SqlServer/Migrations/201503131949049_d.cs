namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblProduct", "InsertDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblProduct", "InsertDate", c => c.DateTime(nullable: false));
        }
    }
}
