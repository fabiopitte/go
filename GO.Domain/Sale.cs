using System.Collections.Generic;

namespace GO.Domain
{
    public class Sale
    {
        public Sale()
        {
            this.Itens = new List<Item>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public byte Type { get; set; }

        public System.DateTime Date { get; set; }

        public System.DateTime? DateDispatch { get; set; }

        public string Observations { get; set; }

        public byte Payment { get; set; }

        public byte PaymentType { get; set; }

        public byte Times { get; set; }

        public string Amount { get; set; }

        public int Quantity { get; set; }

        public string Discount { get; set; }

        public string Total { get; set; }

        public virtual ICollection<Item> Itens { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public Response Response { get; set; }
    }
}
