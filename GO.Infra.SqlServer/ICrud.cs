using System.Collections.Generic;
using System.Linq;

namespace GO.Infra.SqlServer
{
    interface ICrud<T>
    {
        T Get(int id);

        IQueryable<T> Search(T t);

        IQueryable<T> SearchWithInclude(T t, string include);

        T Update(T t);

        T Add(T t);

        bool Delete(int id);
    }
}
