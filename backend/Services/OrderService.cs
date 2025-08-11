using System.Collections.Generic;
using Entities.Models;
using Repository.Contrats;
using Services.Contrats;

namespace Services
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
