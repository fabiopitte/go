namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setimo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblUSer", "Photo_Id", "dbo.tblPhoto");
            DropIndex("dbo.tblUSer", new[] { "Photo_Id" });
            DropColumn("dbo.tblUSer", "Photo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblUSer", "Photo_Id", c => c.Int());
            CreateIndex("dbo.tblUSer", "Photo_Id");
            AddForeignKey("dbo.tblUSer", "Photo_Id", "dbo.tblPhoto", "Id");
        }
    }
}
