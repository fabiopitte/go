using GO.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GO.Infra.SqlServer
{
    public class Repository<T> : DAD<T> where T : new()
    {
        public new IEnumerable<T> Search(T t)
        {
            return base.Search(t).ToList().AsEnumerable();
        }

        public new IEnumerable<T> SearchWithInclude(T t, string include)
        {
            return base.SearchWithInclude(t, include).ToList().AsEnumerable();
        }

        public override T Add(T t)
        {
            return base.Add(t);
        }

        public override T Update(T t)
        {
            return base.Update(t);
        }

        public override T Get(int id)
        {
            return base.Get(id);
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }

        public override Domain.User Login(Domain.User user)
        {
            return base.Login(user);
        }

        public new IEnumerable<GO.Domain.Item> SearchSaleItemsByCustomer(int customerId)
        {
            return base.SearchSaleItemsByCustomer(customerId).ToList().AsEnumerable();
        }

        public new Sale SearchSaleItemsBySale(int saleId)
        {
            var custom = base.SearchSaleItemsBySale(saleId);

            var sale = new Sale();

            custom.ToList().ForEach(e =>
            {
                sale.Id = int.Parse(e.GetType().GetProperty("SaleId").GetValue(e, new object[] { }).ToString());
                sale.Date = System.DateTime.Parse(e.GetType().GetProperty("Date").GetValue(e, new object[] { }).ToString());

                sale.Customer = new Customer
                {
                    CNPJ = e.GetType().GetProperty("Cnpj").GetValue(e, new object[] { }).ToString(),
                    Endereco = new Address
                    {
                        Street = e.GetType().GetProperty("Endereco").GetValue(e, new object[] { }).ToString()
                    }
                };

                var item = (Item)e.GetType().GetProperty("Item").GetValue(e, new object[] { });
                item.Product = (Product)e.GetType().GetProperty("Products").GetValue(e, new object[] { });

                sale.Itens = new List<Item>();
                sale.Itens.Add(item);
            });

            return sale;
        }
    }
}