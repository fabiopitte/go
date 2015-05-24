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

        public new List<Sale> SearchSaleItemsByCustomer(int customerId)
        {
            var saled = base.SearchSaleItemsByCustomer(customerId);

            var sales = new List<Sale>();

            saled.ToList().ForEach(e =>
            {
                var item = e.GetType().GetProperty("Item").GetValue(e, new object[] { });

                var sale = new Sale()
                {
                    Id = int.Parse(e.GetType().GetProperty("SaleId").GetValue(e, new object[] { }).ToString()),
                    Date = System.DateTime.Parse(e.GetType().GetProperty("Date").GetValue(e, new object[] { }).ToString()),
                    DateDispatch = System.DateTime.Parse(e.GetType().GetProperty("DateDispatch").GetValue(e, new object[] { }).ToString()),
                    Observations = System.Convert.ToString(e.GetType().GetProperty("Observacao").GetValue(e, new object[] { })),
                    Customer = new Customer
                    {
                        CPF = System.Convert.ToString(e.GetType().GetProperty("Cpf").GetValue(e, new object[] { })),
                        Nome = System.Convert.ToString(e.GetType().GetProperty("Nome").GetValue(e, new object[] { })),
                        RG = System.Convert.ToString(e.GetType().GetProperty("Rg").GetValue(e, new object[] { })),
                        Telefone = System.Convert.ToString(e.GetType().GetProperty("Telefone").GetValue(e, new object[] { })),
                        Endereco = new Address
                        {
                            Street = System.Convert.ToString(e.GetType().GetProperty("Endereco").GetValue(e, new object[] { })),
                            Number = System.Convert.ToString(e.GetType().GetProperty("Numero").GetValue(e, new object[] { })),
                            CEP = System.Convert.ToString(e.GetType().GetProperty("CEP").GetValue(e, new object[] { })),
                            City = System.Convert.ToString(e.GetType().GetProperty("Cidade").GetValue(e, new object[] { })),
                            State = System.Convert.ToString(e.GetType().GetProperty("Estado").GetValue(e, new object[] { }))
                        }
                    },
                    Itens = new List<Item> 
                    {
                        new Item
                        {
                            Id = int.Parse(item.GetType().GetProperty("ItemId").GetValue(item, new object[] { }).ToString()),
                            ProductId = int.Parse(item.GetType().GetProperty("ProductId").GetValue(item, new object[] { }).ToString()),
                            ProductCode = System.Convert.ToString(item.GetType().GetProperty("ProductCode").GetValue(item, new object[] { })),
                            ProductTitle = System.Convert.ToString(item.GetType().GetProperty("ProductTitle").GetValue(item, new object[] { })),
                            ProductColor = System.Convert.ToString(item.GetType().GetProperty("ProductColor").GetValue(item, new object[] { })),
                            ProductBrand = System.Convert.ToString(item.GetType().GetProperty("ProductBrand").GetValue(item, new object[] { })),
                            Price = System.Convert.ToString(item.GetType().GetProperty("Price").GetValue(item, new object[] { })),
                            Quantity = int.Parse(item.GetType().GetProperty("Quantity").GetValue(item, new object[] { }).ToString())
                        }
                    }
                };

                sales.Add(sale);
            });

            return sales;
        }

        public new Sale SearchSaleItemsBySale(int saleId)
        {
            var custom = base.SearchSaleItemsBySale(saleId);

            var sale = new Sale();

            custom.ToList().ForEach(e =>
            {
                sale.Id = int.Parse(e.GetType().GetProperty("SaleId").GetValue(e, new object[] { }).ToString());
                sale.Date = System.DateTime.Parse(e.GetType().GetProperty("Date").GetValue(e, new object[] { }).ToString());
                sale.Observations = System.Convert.ToString(e.GetType().GetProperty("Observacao").GetValue(e, new object[] { }));

                sale.Customer = new Customer
                {
                    CPF = System.Convert.ToString(e.GetType().GetProperty("Cpf").GetValue(e, new object[] { })),
                    Nome = System.Convert.ToString(e.GetType().GetProperty("Nome").GetValue(e, new object[] { })),
                    RG = System.Convert.ToString(e.GetType().GetProperty("Rg").GetValue(e, new object[] { })),
                    Telefone = System.Convert.ToString(e.GetType().GetProperty("Telefone").GetValue(e, new object[] { })),
                    Endereco = new Address
                    {
                        Street = System.Convert.ToString(e.GetType().GetProperty("Endereco").GetValue(e, new object[] { })),
                        Number = System.Convert.ToString(e.GetType().GetProperty("Numero").GetValue(e, new object[] { })),
                        CEP = System.Convert.ToString(e.GetType().GetProperty("CEP").GetValue(e, new object[] { })),
                        City = System.Convert.ToString(e.GetType().GetProperty("Cidade").GetValue(e, new object[] { })),
                        State = System.Convert.ToString(e.GetType().GetProperty("Estado").GetValue(e, new object[] { }))
                    }
                };
                var item = e.GetType().GetProperty("Item").GetValue(e, new object[] { });

                sale.Itens.Add(new Item
                {
                    ProductId = int.Parse(item.GetType().GetProperty("ProductId").GetValue(item, new object[] { }).ToString()),
                    ProductCode = System.Convert.ToString(item.GetType().GetProperty("ProductCode").GetValue(item, new object[] { })),
                    ProductTitle = item.GetType().GetProperty("ProductTitle").GetValue(item, new object[] { }).ToString(),
                    ProductColor = System.Convert.ToString(item.GetType().GetProperty("ProductColor").GetValue(item, new object[] { })),
                    Price = System.Convert.ToString(item.GetType().GetProperty("Price").GetValue(item, new object[] { })),
                    Quantity = int.Parse(item.GetType().GetProperty("Quantity").GetValue(item, new object[] { }).ToString()),
                });
            });

            return sale;
        }
    }
}