using BethanysPieShop.InventoryManagement;
using System;
using System.Text;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintWelcome();
            Console.WriteLine("Press any key to log in");
            Console.ReadLine();

            Utilities.InitializeInventory();
            Utilities.ShowMainMenu();   

            Console.WriteLine("Application shutting down...");

            Console.ReadLine();

            #region
            void PrintWelcome()
            {
                  Console.ForegroundColor=ConsoleColor.Red;


                Console.OutputEncoding = Encoding.UTF8; 
                Console.Clear();

                string title = " BETHANY PIE SHOP ";
                int width = Console.WindowWidth;
                int padding = (width - title.Length) / 2;

                string horizontalBorder = new string('═', width - 2);
                string emptyLine = "║" + new string(' ', width - 2) + "║";

                Console.ForegroundColor = ConsoleColor.Yellow;
        
                Console.WriteLine("╔" + horizontalBorder + "╗");
                Console.WriteLine(emptyLine);
                Console.WriteLine("║" + new string(' ', padding) + title + new string(' ', padding) + "║");
                Console.WriteLine(emptyLine);
                Console.WriteLine("╚" + horizontalBorder + "╝");

                Console.ResetColor();

            }
            #endregion
        }
    }
}