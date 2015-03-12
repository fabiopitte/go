using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("tblUSer");

            HasKey(c => c.Id);

            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            Property(c => c.Email).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            Property(c => c.Login).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            
            Property(c => c.Password).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            Property(c => c.DDDTelefone).HasColumnType("varchar").HasMaxLength(3);

            Property(c => c.Telefone).HasColumnType("varchar").HasMaxLength(10);

            Ignore(c => c.Response);
        }
    }
}
