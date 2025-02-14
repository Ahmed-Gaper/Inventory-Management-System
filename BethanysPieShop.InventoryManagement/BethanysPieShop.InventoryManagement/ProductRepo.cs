using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement
{
    public class ProductRepo
    {
        private string directoryPath=@"D:\BithenayPieshop\";
        private string prodcutsfile=@"products.txt";

       
        public  void CheckExistingOfTheFile()
        {
             string path=$"{directoryPath}{prodcutsfile}";

            if(File.Exists(path))
                return;

            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.Create(path);

        }

       public List<Product> LoadProductsFromafile()
        {
            List<Product> products = new List<Product>();

            string path = $"{directoryPath}{prodcutsfile}";
            try
            {
                CheckExistingOfTheFile();

                string[] productsAsString = File.ReadAllLines(path);
                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    bool success = int.TryParse(productSplits[0], out int productId);
                    if (!success)
                    {
                        productId = 0;
                    }

                    string name = productSplits[1];
                    string description = productSplits[2];

                    success = int.TryParse(productSplits[3], out int maxItemsInStock);
                    if (!success)
                    {
                        maxItemsInStock = 100;
                    }

                    success = int.TryParse(productSplits[4], out int itemPrice);
                    if (!success)
                    {
                        itemPrice = 0;
                    }

                    success = Enum.TryParse(productSplits[5], out Currency currency);
                    if (!success)
                    {
                        currency = Currency.Dollar;
                    }


                    success = Enum.TryParse(productSplits[6], out UnitType unitType);
                    if (!success)
                    {
                        unitType = UnitType.PerItem;
                    }

                    string productType = productSplits[7];

                    Product product = null;

                    switch (productType)
                    {
                        case "1":
                            success = int.TryParse(productSplits[8], out int amountPerBox);
                            if (!success)
                            {
                                amountPerBox = 1;
                            }

                            product = new BoxedProduct(productId, name, description,maxItemsInStock, new Price() { ItemPrice = itemPrice, Currency = currency }, amountPerBox);
                            break;

                        case "2":
                            string instractions=productSplits[8];
                            DateTime ExpirationDate=DateTime.Parse(productSplits[9]);
                            product = new FreshProducts(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                        case "3":
                            product = new BulkProduct(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock);
                            break;
                            case "4":
                            product = new RegularProduct(productId, name, description,unitType, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock);
                            break;
                    }

                    //Product product = new Product(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);


                    products.Add(product);

                }
            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsing the file, please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return products;
      }

        public void SaveDateToFile(List<Isaveable>savethem)
        {
            string path=$"{directoryPath}{prodcutsfile}";
            StringBuilder sp=new StringBuilder();
            foreach(Isaveable ob in savethem)
            {
                sp.Append(ob.Saveable());
                sp.AppendLine();
            }
            File.WriteAllText(path,sp.ToString());
        }
    }
}
