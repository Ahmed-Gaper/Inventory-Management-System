using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.Order
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime OrderFulFulmentDate { get; set; }
        public bool Fulfiled { get; set; }= false;

        public Order()
        {
           Id=new Random().Next(9999999);
            
            int numOfSeconds=new Random().Next(100);
            OrderFulFulmentDate=DateTime.Now.AddSeconds(numOfSeconds);

            OrderItems=new List<OrderItem>();
        }

        public string ShowOrderDetails()
        {
            StringBuilder st=new StringBuilder();
            st.Append($"Order ID : {Id}");
            st.Append($" Order Date : {OrderFulFulmentDate.ToShortDateString()}");
            st.AppendLine();

            foreach(OrderItem item in OrderItems)
            {
                st.Append(item.ShowOrderItemDetails());
                st.AppendLine();
            }

            return st.ToString();
        }
    }
}
