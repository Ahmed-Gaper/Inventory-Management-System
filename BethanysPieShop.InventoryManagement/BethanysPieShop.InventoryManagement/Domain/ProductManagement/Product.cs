using BethanysPieShop.InventoryManagement.Domain.General;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public abstract class Product : ICloneable
    {

        public static int stackThreshold=5;
        private int id;
        private string name = string.Empty;
        private string? description;

        public int maxItemsInStock = 0;
         public UnitType UnitType { get; set; }

        public int AmountInStock { get; protected set; }
        public bool IsBelowStockTreshold { get; protected set; }
        public Price Price { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;

                }
            }
        }

       


        public Product(int id) : this(id, string.Empty)
        {
        }


        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, UnitType unitType,Price price ,int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;

            Price=price;


            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }
        public static void ChangeStockThreshold(int val)
        {
            if(val > 0)
            stackThreshold=val;
 
        }

        public virtual void UseProduct(int items)
        {
            Console.WriteLine("Using");
            if (items <= AmountInStock)
            {
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public virtual void IncreaseStock()
        {
            AmountInStock++;
        }

        public virtual void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;

            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordere that couldn't be stored.");
                Console.ReadLine();
            }

            if (AmountInStock > 10)
            {
                IsBelowStockTreshold = false;
            }
        }

        public virtual void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        public virtual string DisplayDetailsShort()
        {
            return $"{Id}. {Name} \n{AmountInStock} items in stock";
        }

        public virtual string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id} {Price} {Name} \n{Description}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockTreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();

        }

        public virtual string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id} {Price} {Name} \n{Description}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockTreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        public void UpdateLowStock()
        {
            if (AmountInStock < stackThreshold)
            {
                IsBelowStockTreshold = true;
            }
        }

        protected virtual void Log(string message)
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        protected virtual string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }

        public abstract object Clone();
        
        
    }
}
