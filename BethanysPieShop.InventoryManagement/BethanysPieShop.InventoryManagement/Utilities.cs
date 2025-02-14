using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.Order;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BethanysPieShop.InventoryManagement
{
    public class Utilities
    {
        private static List<Product> inventoryManagement =new List<Product>();
        private static List<Order> orders=new List<Order>();

        public static void InitializeInventory()
        {
            ProductRepo productRepo=new();
           inventoryManagement=productRepo.LoadProductsFromafile();

            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventoryManagement.Count} items");
            Console.Write($"Press enter to continue.....");

            Console.ReadLine();
            Console.ResetColor();

        }
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");
            Console.ResetColor();

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");

            Console.Write("Your selection: ");
 string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventorymanagement();
                    break;
                case "2":
                    ShowOrderManagement();
                    break;
                case "3":
                   ShowSettingsMenu();
                    break;
                case "4":
                    SaveAllData();
                    break;
                case "0":
                break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public static void ShowInventorymanagement()
        {
             string? userChoice;
            do
            {
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("************************");
            Console.WriteLine("* Inventory Management *");
            Console.WriteLine("************************");
            Console.ResetColor();

            ShowProductOverView();

            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do");
            Console.ResetColor();

            Console.WriteLine("1: View details of product");
            Console.WriteLine("2: Add new product");
            Console.WriteLine("3: Clone product");
            Console.WriteLine("4: View products with low stock");
            Console.WriteLine("0: Back to the main menu");

            userChoice=(Console.ReadLine());

                switch(userChoice)
                {
                    case "1":
                        Viewdetailsofproduct();
                        break;
                        case "2":
                        ShowAddNewProduct();
                        break; 
                        case "3":
                        CloneProduct();
                        break;
                        case "4":
                        ViewProductsWithLowStock();
                        break;
                }

            }while(userChoice!="0");
                    ShowMainMenu();

        }

        public static void ShowProductOverView()
        {
            foreach(Product p in inventoryManagement)
            {
                Console.WriteLine(p.DisplayDetailsShort());
                Console.WriteLine();
            }
        }


        public static void  Viewdetailsofproduct()
        {
            Console.Write("Enter the product id : ");
            string? id=Console.ReadLine();

            if(!string.IsNullOrEmpty(id))
            {
             Console.ForegroundColor=ConsoleColor.Blue;
             Product? selectedProduct = inventoryManagement.Where(p => p.Id == int.Parse(id)).FirstOrDefault();
             Console.WriteLine(selectedProduct.DisplayDetailsFull());
             Console.ResetColor();
             Console.WriteLine();


            Console.WriteLine("What do you want to do");
            Console.WriteLine("1: Use The product");
            Console.WriteLine("2: Back to the inventorymanagment");
            Console.Write("your selection : ");
            int userChoice=int.Parse(Console.ReadLine());
                if(userChoice==1)
                {
                        Console.Write("Enter the amount : ");
                        int amount = int.Parse(Console.ReadLine());
                     selectedProduct.UseProduct(amount);
                       Console.ReadLine();

                }
                else
                 ShowInventorymanagement();

            
            }
            else
                {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Invaild -> you didn't chose any product");
                            Console.ReadLine();

                    ShowInventorymanagement();
                  }


           
        }
        public static void ShowAddNewProduct()
        {
            Console.WriteLine();
            Console.WriteLine("What is the type of the product : ");
            Console.WriteLine("1: Regular product.");
            Console.WriteLine("2: Bulk product.");
            Console.WriteLine("3: Fresh product.");
            Console.WriteLine("4: Boxed product.");
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.Write("your selection : ");
            int userSelection=int.Parse(Console.ReadLine());
            Console.ResetColor();

            if(userSelection !=1 && userSelection!=2 && userSelection!=3 && userSelection!=4)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Wrong selection");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("The product name : " );
            string ? name=Console.ReadLine();
            Console.WriteLine();

            Console.Write("The description : " );
            string ? description=Console.ReadLine();
            Console.WriteLine();
            
            Console.Write("The price : " );
            int  itemprice=int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("The maxamount in stock : " );
            int  maxAmount=int.Parse(Console.ReadLine());
            Console.WriteLine();

            Currency currency=(Currency)Enum.Parse(typeof(Currency),ShowAllCurrencies());

            int id=1;
            if(inventoryManagement.Count()!=0)
            id= inventoryManagement.Max(p => p.Id) + 1;//find highest id and increase with 1*/

            Product? newProduct=null;

            UnitType unitType=UnitType.PerItem;
            if(userSelection==1)
            {
             unitType=(UnitType)Enum.Parse(typeof(UnitType),ShowAllUnitTypies());
            }
            switch(userSelection)
            {
                case 1:
                    newProduct=new RegularProduct(id,name,description,unitType,new Price(itemprice,currency),maxAmount);
                    break;

                     case 2:
                    newProduct = new BulkProduct(id, name, description, new Price(itemprice,currency) , maxAmount);
                    break;
                      case 3:
                    Console.Write("Enter the storage instructions: ");
                    string storageInstructions = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter the expiry date: ");
                    DateTime expiryDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

                   
                    newProduct = new FreshProducts(id, name, description,new Price(itemprice,currency), unitType, maxAmount);

                    FreshProducts? fp = newProduct as FreshProducts;

                    fp.StorgeInstraction = storageInstructions;
                    fp.ExpiredDate = expiryDate;

                    if (newProduct != null)
                        inventoryManagement.Add(fp);

                    //fix so that we don't add it again
                    newProduct = null;

                    break;
                      case 4:
                    Console.Write("Enter the number of items per box: ");
                    int numberInBox = int.Parse(Console.ReadLine() ?? "0");

                    newProduct = new BoxedProduct(id++, name, description,numberInBox,new Price(itemprice,currency), maxAmount);
                    break;


            }
              if (newProduct != null)
                inventoryManagement.Add(newProduct);


        }
        private static string ShowAllCurrencies()
        {
            int i=1;
            foreach(string currency in Enum.GetNames(typeof(Currency)))
            {
                Console.WriteLine($"{i}.{currency}");
                i++;
            }
            string selection =(Console.ReadLine());
            return selection;

        }

        private static string ShowAllUnitTypies()
        {
            int i=1;
            foreach(string unitType in Enum.GetNames(typeof(UnitType)))
            {
                Console.WriteLine($"{i}.{unitType}");
                i++;
            }
            Console.Write("Your selection : ");
            string selection=(Console.ReadLine());
            return selection;

        }
        public static void  ViewProductsWithLowStock()
        {
            List<Product>productsBelowStackTreshold=inventoryManagement.Where(p => p.IsBelowStockTreshold).ToList();

            if(productsBelowStackTreshold.Count>0)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Products below stack treshold : ");
                Console.ResetColor();

                foreach(Product p in inventoryManagement)
                {
                    Console.WriteLine(p.DisplayDetailsShort());
                    Console.WriteLine();
                }
                Console.ReadLine();
   
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.Green;
                Console.WriteLine("There is no item below treshold");
                Console.ResetColor();
                Console.ReadLine();

            }
        }

        public static void  ShowOrderManagement()
        {
             int userChoice;
            do
            {
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("************************");
            Console.WriteLine("* Order Management *");
            Console.WriteLine("************************");
            Console.ResetColor();

            ShowProductOverView();

            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do");
            Console.ResetColor();

            Console.WriteLine("1: Open order overview");
            Console.WriteLine("2: Add new order");
  
            Console.WriteLine("0: Back to the main menu");

            userChoice=int.Parse(Console.ReadLine());

                switch(userChoice)
                {
                    case 1:
                        OpenOrderOverview();
                        break;
                        case 2:
                        AddNewOrder();
                        break; 
                 
                default: 
                     Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ResetColor();
                    break;
                }

            }while(userChoice!=0);
            ShowMainMenu();
        }

        public static void  OpenOrderOverview()
        {

            ShowFulfilledOrders();

            if(orders.Count>0)
            {
                foreach(Order order in orders)
                {
                    Console.WriteLine(order.ShowOrderDetails());
                    Console.WriteLine();
                }

                Console.ReadLine();

            }
            else
            {
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("There is no orders opened");
            Console.ResetColor();
                Console.ReadLine();
            }


            
          
            

        }
        public static void ShowFulfilledOrders()
        {
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("Checking fulfilled orders");
            Console.ResetColor();

            foreach(Order order in orders)
            {
                if(!order.Fulfiled && order.OrderFulFulmentDate< DateTime.Now)
                {
                    foreach(OrderItem item in order.OrderItems)
                    {
                        Product selectedProduct=inventoryManagement.Where(p => p.Id==item.ProductId).FirstOrDefault();
                        selectedProduct.IncreaseStock(item.AmountOrdered);
                    }
                    order.Fulfiled=true;
                }
            }
            orders.RemoveAll(o => o.Fulfiled);
            Console.WriteLine("Fulfilles orders checked");


        }
        public static void AddNewOrder()
        {
            ShowProductOverView();
            Order newOrder=new Order();
            string? id;
            

           do {
                Console.WriteLine("Which product do you want to increase -> (enter 0 if you want to stop adding)");
                Console.Write("Enter the product id : ");

                 id=Console.ReadLine();

                if(id!="0")
                {
                     if(!string.IsNullOrEmpty(id))
                     {
                       Product? selectedProduct = inventoryManagement.Where(p => p.Id == int.Parse(id)).FirstOrDefault();
 

                        Console.Write("Enter the amount you want to add : ");
                         int amount=int.Parse(Console.ReadLine()); 
                    
                         OrderItem item=new OrderItem();
                         item.ProductId=selectedProduct.Id;
                         item.ProductName=selectedProduct.Name;
                         item.AmountOrdered=amount;
                          newOrder.OrderItems.Add(item);

                     }
                     else
                     {
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("Invaild -> you didn't chose any product");
                               Console.ReadLine();

                     }
                

                }

            }while(id!="0");


                        Console.ForegroundColor=ConsoleColor.Green;
                        Console.WriteLine("order creating");
                               Console.ReadLine();
             orders.Add(newOrder);
                    ShowOrderManagement();

            
        }

        public static void CloneProduct()
        {
            Console.Write("Enter the product ID : ");
            string? userSelection=(Console.ReadLine());

            if(!string.IsNullOrWhiteSpace(userSelection))
            {
                Product colneThisProduct=inventoryManagement.Where(p => p.Id == int.Parse(userSelection)).FirstOrDefault();

                if(colneThisProduct!=null)
                {
                    Console.Write("Enter the new ID : ");
                        int new_id=int.Parse(Console.ReadLine());

                    Product instanceOfTheClonedPeoduct=colneThisProduct.Clone() as Product;

                    instanceOfTheClonedPeoduct.Id=new_id;
                    inventoryManagement.Add(instanceOfTheClonedPeoduct);

                }
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Invaild -> you didn't chose any product");
                Console.ReadLine();

                    ShowInventorymanagement();
            }


        }

        public static void SaveAllData()
        {
            List<Isaveable>saveThem=new List<Isaveable>();
            ProductRepo save=new();
            foreach(Isaveable ob in inventoryManagement)
            {
                saveThem.Add(ob);
            }
            save.SaveDateToFile(saveThem);
            
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("Saved the all the data");
            Console.ReadLine();
            Console.ResetColor();

            ShowMainMenu();
        }

         private static void ShowSettingsMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************");
                Console.WriteLine("* Settings *");
                Console.WriteLine("************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Change stock treshold");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangeStockTreshold();
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }

         private static void ShowChangeStockTreshold()
        {
            Console.WriteLine($"Enter the new stock treshold (current value: {Product.stackThreshold}). This applies to all products!");
            Console.Write("New value: ");
            int newValue = int.Parse(Console.ReadLine() ?? "0");
            Product.stackThreshold = newValue;
            Console.WriteLine($"New stock treshold set to {Product.stackThreshold}");

            foreach (var product in inventoryManagement)
            {
                product.UpdateLowStock();
            }

            Console.ReadLine();
        }

    }
}











 
              
              
            

          