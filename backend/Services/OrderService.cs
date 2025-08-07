using System.Collections.Generic;
using erpapi.Models;
using erpapi.Repository;
using erpapi.Services.Contracts;

namespace erpapi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        public void CreateOrder(Order order)
        {
            _repository.CreateOrder(order);
        }
    }
}
