using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class SaleMapping : EntityTypeConfiguration<Sale>
    {
        public SaleMapping()
        {
            ToTable("tblSale");

            HasKey(c => c.Id);

            Property(c => c.CustomerId).IsOptional();

            Ignore(c => c.Response);
        }
    }
}