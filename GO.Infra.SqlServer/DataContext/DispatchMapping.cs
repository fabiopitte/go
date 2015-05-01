using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class DispatchMapping : EntityTypeConfiguration<Dispatch>
    {
        public DispatchMapping()
        {
            ToTable("tblDispatch");

            HasKey(c => c.Id);

            Property(c => c.CustomerId).IsRequired();

            // Property(c => c.Itens).IsRequired();

            Property(c => c.Date).IsRequired();

            Property(c => c.Observations).IsOptional();

            Ignore(c => c.Response);
        }
    }
}