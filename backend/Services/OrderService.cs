using System.Collections.Generic;
using AutoMapper; // AutoMapper eklenmeli
using Entities.Models; // ViewModel ya da dışa açık model
using erpapi.Repository; // Repository ve entity modeli
using Services.Contracts;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            // Repository'den gelen Repository.Order → erpapi.Models.Order'a map edilir
            var repoOrders = _repository.GetAllOrders();
            return _mapper.Map<IEnumerable<Order>>(repoOrders);
        }

        public void CreateOrder(Order order)
        {
            // erpapi.Models.Order → Repository.Order’a map edilir
            var repoOrder = _mapper.Map<Repository.Order>(order);
            _repository.CreateOrder(repoOrder);
        }
    }
}
