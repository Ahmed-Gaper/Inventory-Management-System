using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    internal class BulkProduct : Product,Isaveable
    {
        public BulkProduct(int id,string name,string description,Price price,int maxamount) :base(id,name,description,UnitType.PerKg,price,maxamount)
        {

        }

        public override object Clone()
        {
            return new BulkProduct(0,this.Name,this.Description,this.Price,this.maxItemsInStock);

        }

         public string Saveable()
 {
     return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};{3};";
 }

         
    }
}
