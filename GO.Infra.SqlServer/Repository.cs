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
    }
}