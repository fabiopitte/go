using System.Collections.Generic;

namespace GO.Domain
{
    public class Photo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int ProductId { get; set; }

        public byte[] File { get; set; }

        public virtual Product Product { get; set; }
    }
}