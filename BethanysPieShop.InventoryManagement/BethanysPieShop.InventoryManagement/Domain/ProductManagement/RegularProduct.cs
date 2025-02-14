using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    internal class RegularProduct : Product,Isaveable
    {
          public RegularProduct(int id, string name, string? description, UnitType unitType,Price price ,int maxAmountInStock):base(id,name,description,unitType,price,maxAmountInStock)
        {

        }


        public override object Clone()
        {
            return new RegularProduct(0,this.Name,this.Description,this.UnitType,this.Price,this.maxItemsInStock);
        }

         public string Saveable()
 {
     return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};{4};";
 }
    }

   
}
