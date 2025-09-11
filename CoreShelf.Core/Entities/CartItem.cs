using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Entities
{
    public class CartItem
    {
        public int BookId { get; set; }
        public required string BookTitle { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string PictureUrl { get; set; }
        public required string Category { get; set; }
    }
}
