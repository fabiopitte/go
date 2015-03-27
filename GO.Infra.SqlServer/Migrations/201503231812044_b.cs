namespace GO.Infra.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblCustomer", name: "Endereco_Id", newName: "AddressId");
            RenameColumn(table: "dbo.tblSupplier", name: "Endereco_Id", newName: "AddressId");
            RenameIndex(table: "dbo.tblCustomer", name: "IX_Endereco_Id", newName: "IX_AddressId");
            RenameIndex(table: "dbo.tblSupplier", name: "IX_Endereco_Id", newName: "IX_AddressId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tblSupplier", name: "IX_AddressId", newName: "IX_Endereco_Id");
            RenameIndex(table: "dbo.tblCustomer", name: "IX_AddressId", newName: "IX_Endereco_Id");
            RenameColumn(table: "dbo.tblSupplier", name: "AddressId", newName: "Endereco_Id");
            RenameColumn(table: "dbo.tblCustomer", name: "AddressId", newName: "Endereco_Id");
        }
    }
}
