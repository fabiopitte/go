using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class PhotoMapping : EntityTypeConfiguration<Photo>
    {
        public PhotoMapping()
        {
            ToTable("tblPhoto");

            HasKey(c => c.Id);

            Property(c => c.File).IsRequired();

            Property(c => c.Url).HasColumnType("varchar").HasMaxLength(500).IsRequired();

            Property(c => c.Title).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        }
    }
}