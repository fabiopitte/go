using System.Collections.Generic;

namespace GO.Domain
{
    public class Photo
    {
        public Photo()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
