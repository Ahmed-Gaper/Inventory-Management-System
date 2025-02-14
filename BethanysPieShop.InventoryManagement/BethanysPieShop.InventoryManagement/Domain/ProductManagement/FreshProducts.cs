using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class FreshProducts : Product,Isaveable
    {
        public DateTime ExpiredDate{ get; set; }
        public string? StorgeInstraction{ get; set;}

        public FreshProducts(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : base(id, name, description, unitType,price, maxAmountInStock)
        {

        }

        public override string DisplayDetailsShort()
        {
            StringBuilder sb=new();
            sb.Append(base.DisplayDetailsShort());
            sb.AppendLine();

            sb.Append($"Storage instrections : {StorgeInstraction}");
            sb.AppendLine();
            sb.Append($"Date of expired : {ExpiredDate.ToShortDateString()}");

            return sb.ToString();
        }


        public override object Clone()
        {
            return new FreshProducts(0,this.Name,this.Description,this.Price,this.UnitType,this.maxItemsInStock);
        }
        
            
      public string Saveable()
 {
     return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};{2};{StorgeInstraction};{ExpiredDate};";
 }

    }
}
