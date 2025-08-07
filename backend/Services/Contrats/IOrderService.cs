using System.Collections.Generic;
using erpapi.Models;
using erpapi.Services.Contracts;


using Services.Contracts;

{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
    }
}
