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

            Property(c => c.Title).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        }
    }
}
