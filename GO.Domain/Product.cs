﻿namespace GO.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public decimal Margin { get; set; }

        public int Quantity { get; set; }

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public int? BrandId { get; set; }

        public int? PhotoId { get; set; }

        public string Description { get; set; }
        
        public string Style { get; set; }

        public string Size { get; set; }

        public string Measure { get; set; }

        public string Color { get; set; }

        public string Model { get; set; }

        public virtual Photo Photo { get; set; }

        public virtual Category Category { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Supplier Supplier { get; set; }

        public Response Response { get; set; }
    }
}
