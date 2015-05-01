namespace GO.Domain
{
    public class Item
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int  Quantity { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }
    }
}