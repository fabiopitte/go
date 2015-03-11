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

            Property(c => c.Nome).HasMaxLength(100).IsRequired();
            Property(c => c.Email).HasMaxLength(100);
            Property(c => c.DDDTelefone).HasMaxLength(3);
            Property(c => c.Telefone).HasMaxLength(10);
            Property(c => c.DDDCelular).HasMaxLength(3);
            Property(c => c.Celular).HasMaxLength(10);
            Property(c => c.IE).HasMaxLength(10);
            Property(c => c.Observacoes).HasMaxLength(255);
            Property(c => c.CNPJ).HasMaxLength(14);
            Property(c => c.RazaoSocial).HasMaxLength(100);

            Ignore(c => c.Response);
        }
    }
}