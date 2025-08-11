using System;

namespace Entities.Models
{
    public class OrderItem
    {
        public int ItemID { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
