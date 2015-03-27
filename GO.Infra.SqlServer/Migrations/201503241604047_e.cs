namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblStyle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblProduct", "StyleId", c => c.Int());
            CreateIndex("dbo.tblProduct", "StyleId");
            AddForeignKey("dbo.tblProduct", "StyleId", "dbo.tblStyle", "Id");
            DropColumn("dbo.tblProduct", "Style");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblProduct", "Style", c => c.String(maxLength: 50, unicode: false));
            DropForeignKey("dbo.tblProduct", "StyleId", "dbo.tblStyle");
            DropIndex("dbo.tblProduct", new[] { "StyleId" });
            DropColumn("dbo.tblProduct", "StyleId");
            DropTable("dbo.tblStyle");
        }
    }
}
