using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class ItemMapping : EntityTypeConfiguration<Item>
    {
        public ItemMapping()
        {
            ToTable("tblItem");

            HasRequired(c => c.Sale);

            HasKey(c => c.Id);
        }
    }
}