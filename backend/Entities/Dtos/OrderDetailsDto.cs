using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;   // OrderStatus için

namespace Entities.Dtos
{
    public class OrderDetailsDto
    {
        public int OrderID { get; set; }

        // Nullability uyarısı olmasın diye varsayılan değer verdik
        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        // string değil, enum: derleyicide dönüşüm hatasını engeller
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItemDto> Items { get; set; } = new();

        // Items zaten new() ile boş liste, null kontrolüne gerek yok
        public decimal Subtotal => Items.Sum(i => i.LineTotal);
    }
}
