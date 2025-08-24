using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;
using Entities.Models;

namespace Services.Contrats
{
    public interface IOrderService
    {
        // Eski metotlar (kullanım kırılmasın)
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);

        // Yeni metotlar (Sipariş Detayı akışı)
        Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId);
        Task UpdateOrderAsync(OrderDetailsDto dto);
    }
}
