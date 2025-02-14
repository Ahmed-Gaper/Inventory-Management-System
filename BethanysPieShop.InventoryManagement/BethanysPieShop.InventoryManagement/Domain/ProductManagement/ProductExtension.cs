using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public static class ProductExtension
    {
        private static double fromPoundToEuro=50;
        private static double fromDoularToEuro=1;
        private static double fromDolarToPound=.05;

        public static double ConvertCurrency(this Product product,Currency targrtCurrency)
        {
            double productPrice=product.Price.ItemPrice;
            Currency productCurrency=product.Price.Currency;

            double answer=0.0;
            if(productCurrency==Currency.Pound && targrtCurrency==Currency.euro)
            {
                answer= productPrice*fromPoundToEuro;
            }
            return answer;
        }

    }
}
