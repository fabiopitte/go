using System.Collections.Generic;

namespace GO.Domain
{
    public class Dispatch
    {
        public Dispatch()
        {
            this.Itens = new List<Item>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public Response Response { get; set; }

        public virtual ICollection<Item> Itens { get; set; }

        public System.DateTime Date { get; set; }

        public string Observations { get; set; }
    }
}
