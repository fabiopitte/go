using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class AddressMapping : EntityTypeConfiguration<Address>
    {
        public AddressMapping()
        {
            ToTable("tblAddress");

            Property(c => c.Street).HasMaxLength(100).HasColumnType("varchar");
            Property(c => c.Number).HasMaxLength(50).HasColumnType("varchar");
            Property(c => c.CEP).HasMaxLength(10).HasColumnType("varchar");
            Property(c => c.District).HasMaxLength(50).HasColumnType("varchar");
            Property(c => c.City).HasMaxLength(50).HasColumnType("varchar");
            Property(c => c.Estate).HasMaxLength(50).HasColumnType("varchar");
            Property(c => c.Complement).HasMaxLength(100).HasColumnType("varchar");

            HasKey(c => c.Id);
        }
    }
}