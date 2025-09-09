using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public required string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public required string Category { get; set; }
    }
}
