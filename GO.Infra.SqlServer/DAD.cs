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
    }
}