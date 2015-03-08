using System.Collections.Generic;

namespace GO.Infra
{
    interface ICrud<T>
    {
        T Get(string id);

        IEnumerable<T> Search(T t);

        bool Update(T t);

        T Add(T t);

        List<T> Add(List<T> t);

        bool Delete(string id);
    }
}
