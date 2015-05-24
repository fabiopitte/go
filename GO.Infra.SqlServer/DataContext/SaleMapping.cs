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

            Property(c => c.CustomerId).IsRequired();

            Property(c => c.Type).IsRequired();

            Property(c => c.Date).IsRequired();

            Property(c => c.DateDispatch).IsOptional();

            Property(c => c.Observations).IsOptional();

            Property(c => c.Discount).IsOptional();

            Property(c => c.Payment).IsRequired();

            Property(c => c.PaymentType).IsRequired();

            Property(c => c.Amount).IsRequired();

            Property(c => c.Quantity).IsRequired();

            Property(c => c.Total).IsRequired();

            Property(c => c.UserId).IsRequired();

            Ignore(c => c.Response);
        }
    }
}