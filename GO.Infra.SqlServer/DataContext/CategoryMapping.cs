using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            ToTable("tblCategory");

            HasKey(c => c.Id);

            Property(c => c.Title).HasMaxLength(100).IsRequired();

            Ignore(c => c.Response);
        }
    }
}
