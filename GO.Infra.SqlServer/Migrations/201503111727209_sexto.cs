namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sexto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUSer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Login = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        DDDTelefone = c.String(maxLength: 3, unicode: false),
                        Telefone = c.String(maxLength: 10, unicode: false),
                        Photo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblPhoto", t => t.Photo_Id)
                .Index(t => t.Photo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUSer", "Photo_Id", "dbo.tblPhoto");
            DropIndex("dbo.tblUSer", new[] { "Photo_Id" });
            DropTable("dbo.tblUSer");
        }
    }
}
