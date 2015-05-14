namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblDispatch", "CustomerId", "dbo.tblCustomer");
            DropForeignKey("dbo.tblItem", "Dispatch_Id", "dbo.tblDispatch");
            DropIndex("dbo.tblDispatch", new[] { "CustomerId" });
            DropIndex("dbo.tblItem", new[] { "Dispatch_Id" });
            DropColumn("dbo.tblItem", "Dispatch_Id");
            DropTable("dbo.tblDispatch");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblDispatch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Observations = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblItem", "Dispatch_Id", c => c.Int());
            CreateIndex("dbo.tblItem", "Dispatch_Id");
            CreateIndex("dbo.tblDispatch", "CustomerId");
            AddForeignKey("dbo.tblItem", "Dispatch_Id", "dbo.tblDispatch", "Id");
            AddForeignKey("dbo.tblDispatch", "CustomerId", "dbo.tblCustomer", "Id", cascadeDelete: true);
        }
    }
}
