using CoreShelf.API.DTOs;
using CoreShelf.API.Extensions;
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
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders([FromQuery] OrderSpecParams specParams)
        {
            var spec = new OrderSpecification(specParams);

            return await CreatePagedResult(unit.Repository<Order>(), spec, specParams.PageIndex, specParams.PageSize, o => o.ToDto());
        }

        [HttpGet("orders/{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(id);

            var order = await unit.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
            {
                return BadRequest("No order with that id");
            }

            return order.ToDto();
        }
    }
}
