using CoreShelf.API.DTOs;
using CoreShelf.Core.Entities.OrderAggregate;

namespace CoreShelf.API.Extensions
{
    public static class OrderMappingExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                BuyerEmail = order.BuyerEmail,
                ShippingAddress = order.ShippingAddress,
                DeliveryMethod = order.DeliveryMethod.Description,
                PaymentSummary = order.PaymentSummary,
                Subtotal = order.Subtotal,
                Status = order.Status.ToString(),
                PaymentIntentId = order.PaymentIntentId,
                Total = order.GetTotal(),
                OrderItems = order.OrderItems.Select(x => x.ToDto()).ToList(),
                ShippingPrice = order.DeliveryMethod.Price
            };
        }

        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductId = orderItem.ItemOrdered.ProductId,
                ProductName = orderItem.ItemOrdered.ProductName,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity
            };
        }
    }
}
