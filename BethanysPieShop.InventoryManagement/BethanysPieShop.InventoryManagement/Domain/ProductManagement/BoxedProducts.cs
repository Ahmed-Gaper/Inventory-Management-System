using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class BoxedProduct : Product,Isaveable
    {
        public int ItemPerBox { get; set; }

        public BoxedProduct(int id,string name,string descriprion,int itemPerBox,Price price,int maxamountInStock) : base(id,name,descriprion,UnitType.PerBox,price,maxamountInStock)
        {
            ItemPerBox=itemPerBox;
        }

         public override void UseProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * ItemPerBox > items)
                {
                    batchSize = smallestMultiple * ItemPerBox;
                    break;
                }
            }
            base.UseProduct(batchSize);
        }

        public override string DisplayDetailsFull()
        {
            StringBuilder st=new();
            st.Append("Boxed product ");
            st.Append($"{Id} {Price} {Name} \n{Description}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockTreshold)
            {
                st.Append("\n!!STOCK LOW!!");
            }

            return st.ToString(); 
        }

        public override void IncreaseStock()
        {
            IncreaseStock(1);
        }

        public override void IncreaseStock(int amount)
        {
           
            int newStock = AmountInStock + amount * ItemPerBox;

            if (newStock <= maxItemsInStock)
            {     
                AmountInStock += amount * ItemPerBox;
            }
            else
            {
                          
                AmountInStock = maxItemsInStock;//we only store the possible items, overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordere that couldn't be stored.");
                Console.ReadLine();
            }

            if (AmountInStock > stackThreshold)
            {
                IsBelowStockTreshold = false;
            }
        }

        public override object Clone()
        {
            return new BoxedProduct(0,this.Name,this.Description,this.ItemPerBox,this.Price,this.maxItemsInStock);

        }

        public string Saveable()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};{1};{ItemPerBox};";
        }


    }



}
