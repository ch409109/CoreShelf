using CoreShelf.Core.Entities;
using CoreShelf.Core.Entities.OrderAggregate;

namespace CoreShelf.API.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string BuyerEmail { get; set; }
        public required ShippingAddress ShippingAddress { get; set; } = null!;
        public required string DeliveryMethod { get; set; } = null!;
        public decimal ShippingPrice { get; set; }
        public required PaymentSummary PaymentSummary { get; set; } = null!;
        public required List<OrderItemDto> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public required string Status { get; set; }
        public decimal Total { get; set; }
        public required string PaymentIntentId { get; set; }
    }
}
