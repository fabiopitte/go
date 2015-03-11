using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            ToTable("tblProduct");

            HasKey(c => c.Id);

            Property(c => c.Title).HasMaxLength(100).IsRequired();

            Property(c => c.Description).HasMaxLength(255);

            Property(c => c.Tamanho).HasMaxLength(10);

            HasRequired(c => c.Category);

            Ignore(c => c.Response);
        }
    }
}
