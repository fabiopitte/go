namespace GO.Infra.SqlServer.Migrations
{
    using GO.Domain;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GO.Infra.SqlServer.DataContext.GoStoreDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GO.Infra.SqlServer.DataContext.GoStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Vestidos" });
            context.Categories.Add(new Category { Id = 2, Title = "Sapatos" });
            context.Categories.Add(new Category { Id = 3, Title = "Acessorios" });

            context.User.Add(new User { Id = 1, Name = "Fabio Pitte", Login = "admin", Password = "123", Email = "fabiopitte@gmail.com", DDDTelefone = "11", Telefone = "966631980" });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
