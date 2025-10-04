using CoreShelf.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string email) : base(x => x.BuyerEmail == email)
        {

        }
        public OrderSpecification(string email, int id) : base(x => x.BuyerEmail == email && x.Id == id)
        {

        }
    }
}
