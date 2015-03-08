using System.Collections.Generic;

namespace GO.Infra
{
    public class Repository<T> : DAD<T> where T : new()
    {
        public override IEnumerable<T> Search(T t)
        {
            return base.Search(t);
        }
    }
}