namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblAddress", "State", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.tblAddress", "Estate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblAddress", "Estate", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.tblAddress", "State");
        }
    }
}
