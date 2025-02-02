using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.General
{
    public class Price
    {
        public int ItemPrice { get; set; }
        public Currency Currency { get; set; }
         public Price( )
        {
           
        }
        public Price(int itemPrice,Currency currency )
        {
            ItemPrice=itemPrice;
            Currency=Currency;
        } 
       
        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }
    }
}
