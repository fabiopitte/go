using System;
using GO.Domain;
using System.Data.Entity;

namespace GO.Infra.SqlServer.DataContext
{
    public class GoStoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public GoStoreDataContext()
            : base("name=GOStoreConnectionString")
        {
            Database.SetInitializer<GoStoreDataContext>(new GostoreDataContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new SupplierMapping());
            modelBuilder.Configurations.Add(new CustomerMapping());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class GostoreDataContextInitializer : DropCreateDatabaseIfModelChanges<GoStoreDataContext>
    {
        protected override void Seed(GoStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Vestidos" });
            context.Categories.Add(new Category { Id = 2, Title = "Sapatos" });
            context.Categories.Add(new Category { Id = 3, Title = "Acessorios" });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
