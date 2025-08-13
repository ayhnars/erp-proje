using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repository.Contrats;   // IOrderRepository, IOrderItemRepository
using Services.Contrats;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orders;
        private readonly IOrderItemRepository _items;
        private readonly IMapper _mapper;

        // NOT: AutoMapper ve OrderItemRepository ekledik
        public OrderService(IOrderRepository orders, IOrderItemRepository items, IMapper mapper)
        {
            _orders = orders;
            _items = items;
            _mapper = mapper;
        }

        // --- Eski metotlar (kullanmaya devam edebilirsin) ---
        public IEnumerable<Order> GetAllOrders() => _orders.GetAllOrders();

        public void CreateOrder(Order order) => _orders.CreateOrder(order);

        // --- Yeni akış: Detay + Güncelle ---
        public async Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId)
        {
            var order = await _orders.GetByIdAsync(orderId);
            if (order == null) return null;

            var dto = _mapper.Map<OrderDetailsDto>(order);
            var items = await _items.GetByOrderIdAsync(orderId);
            dto.Items = _mapper.Map<List<OrderItemDto>>(items);
            return dto;
        }

        public async Task UpdateOrderAsync(OrderDetailsDto dto)
        {
            var order = await _orders.GetByIdAsync(dto.OrderID);
            if (order == null) throw new System.InvalidOperationException("Order not found");

            // Başlık alanları (ihtiyacına göre genişlet)
            order.OrderDate = dto.OrderDate;
            order.Status = dto.Status;

            await _orders.UpdateAsync(order);

            // Kalemler: ItemID==0 → ekle, >0 → güncelle
            foreach (var i in dto.Items)
            {
                if (i.ItemID == 0)
                {
                    var newItem = _mapper.Map<OrderItem>(i);
                    newItem.OrderID = dto.OrderID;
                    await _items.InsertAsync(newItem);
                }
                else
                {
                    var existing = await _items.GetByIdAsync(i.ItemID);
                    if (existing == null) continue;
                    existing.ProductID = i.ProductID;
                    existing.UnitPrice = i.UnitPrice;
                    existing.Quantity = i.Quantity;
                    await _items.UpdateAsync(existing);
                }
            }
        }
    }
}
