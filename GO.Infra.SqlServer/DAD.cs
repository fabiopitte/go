using GO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GO.Infra.SqlServer
{
    public abstract class DAD<T> : ICrud<T>
    {
        public GO.Infra.SqlServer.DataContext.GoStoreDataContext db { get; set; }

        public DAD()
        {
            db = new GO.Infra.SqlServer.DataContext.GoStoreDataContext();
        }

        public virtual T Get(int id)
        {
            try
            {
                var obj = db.Set(typeof(T)).Find(id);

                return (T)obj;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual IQueryable<T> Search(T t)
        {
            try
            {
                var consulta = db.Set(typeof(T)).AsQueryable();

                return (IQueryable<T>)consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual IQueryable<T> SearchWithInclude(T t, string include)
        {
            try
            {
                var consulta = db.Set(typeof(T)).Include(include).AsQueryable();

                return (IQueryable<T>)consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual T Add(T t)
        {
            try
            {
                db.Set(typeof(T)).Add(t);

                db.SaveChanges();

                return t;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual T Update(T t)
        {
            db.Entry((object)t).State = EntityState.Modified;

            db.SaveChanges();

            return t;
        }

        public virtual bool Delete(int id)
        {
            var obj = db.Set(typeof(T)).Find(id);

            db.Set(typeof(T)).Remove((T)obj);

            db.SaveChanges();

            return true;
        }

        public virtual GO.Domain.User Login(GO.Domain.User user)
        {
            try
            {
                var consulta = db.User.FirstOrDefault(usuario => usuario.Login.Equals(user.Login) && usuario.Password.Equals(user.Password));

                return consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<GO.Domain.Customer> Search(GO.Domain.Customer customer, int limit)
        {
            try
            {
                var consulta = db.Customer.Where(cust => cust.Nome.Contains(customer.Nome)).Take(limit).ToList();

                return consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<GO.Domain.Product> Search(GO.Domain.Product product, int limit)
        {
            try
            {
                var consulta = db.Products
                    .Where(cust => cust.Code.Contains(product.Code) || cust.Title.Contains(product.Title))
                    .Where(cust => cust.Quantity > 0)
                    .Take(limit)
                    .ToList();

                return consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual IQueryable<object> SearchSaleItemsByCustomer(int customerId)
        {
            try
            {
                var consulta = from sale in db.Sale
                               join item in db.Item on sale.Id equals item.SaleId
                               join product in db.Products on item.ProductId equals product.Id
                               join brand in db.Brand on product.BrandId equals brand.Id
                               where sale.CustomerId == customerId && item.ProductDispatched == false
                               select new
                               {
                                   SaleId = sale.Id,
                                   Date = sale.Date,
                                   DateDispatch = sale.DateDispatch,
                                   Observacao = sale.Observations,
                                   Nome = sale.Customer.Nome,
                                   Cpf = sale.Customer.CPF,
                                   Rg = sale.Customer.RG,
                                   Telefone = sale.Customer.Telefone,
                                   Endereco = sale.Customer.Endereco.Street,
                                   Numero = sale.Customer.Endereco.Number,
                                   CEP = sale.Customer.Endereco.CEP,
                                   Cidade = sale.Customer.Endereco.City,
                                   Estado = sale.Customer.Endereco.State,
                                   Item = new
                                   {
                                       ItemId = item.Id,
                                       ProductCode = product.Code,
                                       ProductTitle = product.Title,
                                       ProductColor = product.Color,
                                       ProductBrand = brand.Title,
                                       Price = item.Price,
                                       ProductId = product.Id,
                                       Quantity = item.Quantity
                                   }
                               };

                return consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual IQueryable<object> SearchSaleItemsBySale(int saleId)
        {
            try
            {
                var consulta = from sale in db.Sale
                               join item in db.Item on sale.Id equals item.SaleId
                               join product in db.Products on item.ProductId equals product.Id
                               where sale.Id == saleId
                               select new
                               {
                                   SaleId = sale.Id,
                                   Date = sale.Date,
                                   Observacao = sale.Observations,
                                   Nome = sale.Customer.Nome,
                                   Cpf = sale.Customer.CPF,
                                   Rg = sale.Customer.RG,
                                   Telefone = sale.Customer.Telefone,
                                   Endereco = sale.Customer.Endereco.Street,
                                   Numero = sale.Customer.Endereco.Number,
                                   CEP = sale.Customer.Endereco.CEP,
                                   Cidade = sale.Customer.Endereco.City,
                                   Estado = sale.Customer.Endereco.State,
                                   Item = new
                                   {
                                       ProductCode = product.Code,
                                       ProductTitle = product.Title,
                                       ProductColor = product.Color,
                                       Price = item.Price,
                                       ProductId = product.Id,
                                       Quantity = item.Quantity
                                   }
                               };

                return consulta;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<GO.Domain.Photo> Search(int productId)
        {
            try
            {
                var consulta = db.Photo.Where(p => p.ProductId == productId);

                return consulta.ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}