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

            Property(c => c.Title).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            Property(c => c.Cost).IsOptional();

            Property(c => c.Margin).IsOptional();

            Property(c => c.SupplierId).IsOptional();

            Property(c => c.PhotoId).IsOptional();

            Property(c => c.Description).HasColumnType("varchar").HasMaxLength(255);

            Property(c => c.Size).HasColumnType("varchar").HasMaxLength(50);

            Property(c => c.Measure).HasColumnType("varchar").HasMaxLength(50);

            Property(c => c.Color).HasColumnType("varchar").HasMaxLength(50);

            Property(c => c.Model).HasColumnType("varchar").HasMaxLength(50);

            Property(c => c.Style).HasColumnType("varchar").HasMaxLength(50);

            HasRequired(c => c.Category);

            Ignore(c => c.Response);
        }
    }
}
