﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
