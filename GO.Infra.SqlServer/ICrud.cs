using System.Collections.Generic;
using System.Linq;

namespace GO.Infra.SqlServer
{
    interface ICrud<T>
    {
        T Get(int id);

        IQueryable<T> Search(T t);

        T Update(T t);

        T Add(T t);

        bool Delete(int id);
    }
}
