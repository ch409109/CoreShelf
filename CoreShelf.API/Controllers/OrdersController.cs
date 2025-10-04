using CoreShelf.API.Extensions;
using CoreShelf.Core.Entities;
using CoreShelf.Core.Entities.OrderAggregate;
using CoreShelf.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreShelf.API.Controllers
{
    public class OrdersController(ICartService cartService, IUnitOfWork unit) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto orderDto)
        {
            var email = User.GetEmail();

            var cart = await cartService.GetCartAsync(orderDto.CartId);

            if (cart == null)
            {
                return BadRequest("Cart not found");
            }

            if (cart.PaymentIntentId == null)
            {
                return BadRequest("No payment intent for this order");
            }

            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var productItem = await unit.Repository<Product>().GetByIdAsync(item.ProductId);

                if (productItem == null)
                {
                    return BadRequest("Problem with the order");
                }

                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl
                };

                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await unit.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null)
            {
                return BadRequest("No delivery method selected");
            }

            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderDto.ShippingAddress,
                Subtotal = items.Sum(item => item.Price * item.Quantity),
                PaymentIntentId = cart.PaymentIntentId,
                PaymentSummary = orderDto.PaymentSummary,
                BuyerEmail = email
            };

            unit.Repository<Order>().Add(order);

            if (await unit.Complete())
            {
                return order;
            }

            return BadRequest("Problem creating order");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());

            var orders = await unit.Repository<Order>().ListAsync(spec);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(User.GetEmail(), id);

            var order = await unit.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
    }
}
