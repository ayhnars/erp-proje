using System.Collections.Generic;
<<<<<<< HEAD
using erpapi.Models;
using erpapi.Services.Contracts;


using Services.Contracts;

{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
=======
using System.Threading.Tasks;
using Entities.Dtos;
using Entities.Models;

namespace Services.Contrats
{
    public interface IOrderService
    {
        // Eski metotlar kalsın (kullandığın yerleri bozmasın)
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);

        // Sipariş Detayı ekranı için yeni metotlar
        Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId);
        Task UpdateOrderAsync(OrderDetailsDto dto);
>>>>>>> order
    }
}
