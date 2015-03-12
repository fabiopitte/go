namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblProduct", "PhotoId", "dbo.tblPhoto");
            DropIndex("dbo.tblProduct", new[] { "PhotoId" });
            AlterColumn("dbo.tblProduct", "PhotoId", c => c.Int());
            CreateIndex("dbo.tblProduct", "PhotoId");
            AddForeignKey("dbo.tblProduct", "PhotoId", "dbo.tblPhoto", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProduct", "PhotoId", "dbo.tblPhoto");
            DropIndex("dbo.tblProduct", new[] { "PhotoId" });
            AlterColumn("dbo.tblProduct", "PhotoId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProduct", "PhotoId");
            AddForeignKey("dbo.tblProduct", "PhotoId", "dbo.tblPhoto", "Id", cascadeDelete: true);
        }
    }
}
