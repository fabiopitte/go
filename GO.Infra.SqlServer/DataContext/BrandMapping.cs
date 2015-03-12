using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class BrandMapping : EntityTypeConfiguration<Brand>
    {
        public BrandMapping()
        {
            ToTable("tblBrand");

            HasKey(c => c.Id);

            Property(c => c.Title).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            Ignore(c => c.Response);
        }
    }
}