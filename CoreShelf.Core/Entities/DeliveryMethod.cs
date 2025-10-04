using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
