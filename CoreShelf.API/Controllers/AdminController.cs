using CoreShelf.Core.Entities.OrderAggregate;
using CoreShelf.Core.Specifications;
using CoreShelf.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreShelf.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(UnitOfWork unit) : BaseApiController
    {
        [HttpGet("orders")]
        public async Task<ActionResult<Order>> GetOrders([FromQuery] OrderSpecParams specParams)
        {
            var spec = new OrderSpecification(specParams);

            return await CreatePagedResult(unit.Repository<Order>(), spec, specParams.PageIndex, specParams.PageSize);
        }
    }
}
