﻿using CoreShelf.Core.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace CoreShelf.API.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public string CartId { get; set; } = string.Empty;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public ShippingAddress ShippingAddress { get; set; } = null;
        [Required]
        public PaymentSummary PaymentSummary { get; set; } = null;
    }
}
