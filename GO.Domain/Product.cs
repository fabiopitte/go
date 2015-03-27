using System;
using System.Collections.Generic;

namespace GO.Domain
{
    public class Product
    {
        public Product()
        {
            this.Photos = new List<Photo>();
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public DateTime? InsertDate { get; set; }

        public int Quantity { get; set; }

        public int? StyleId { get; set; }

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public int? BrandId { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string Measure { get; set; }

        public string Color { get; set; }

        public string Model { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual Category Category { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Style Style { get; set; }

        public Response Response { get; set; }
    }
}