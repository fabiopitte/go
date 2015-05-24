using GO.Domain;
using System.Data.Entity.ModelConfiguration;

namespace GO.Infra.SqlServer.DataContext
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("tblCustomer");

            HasKey(c => c.Id);

            Property(c => c.Nome).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(c => c.Email).HasColumnType("varchar").HasMaxLength(100);
            Property(c => c.DDDTelefone).HasColumnType("varchar").HasMaxLength(3);
            Property(c => c.Telefone).HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.DDDCelular).HasColumnType("varchar").HasMaxLength(3);
            Property(c => c.Celular).HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.IE).HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.Observacoes).HasColumnType("varchar").HasMaxLength(255);
            Property(c => c.CPF).HasColumnType("varchar").HasMaxLength(14);
            Property(c => c.RazaoSocial).HasColumnType("varchar").HasMaxLength(100);
            Property(c => c.RG).HasColumnType("varchar").HasMaxLength(100);

            Property(c => c.AddressId).IsOptional();

            Ignore(c => c.Response);
        }
    }
}