using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int AmountOrdered { get; set; }

        public string ShowOrderItemDetails()
        {
            return $"product ID : {ProductId}  Amount ordered : {AmountOrdered} ";
        }
    }
}
