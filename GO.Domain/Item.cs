namespace GO.Domain
{
    public class Item
    {
        public int Id { get; set; }

        public string Price { get; set; }

        public int Quantity { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductColor { get; set; }

        public string ProductBrand { get; set; }

        public string ProductTitle { get; set; }

        private bool productDispatched = false;

        public bool ProductDispatched { get { return productDispatched; } set { productDispatched = value; } }

        public System.DateTime? DateDispatched { get; set; }
    }
}