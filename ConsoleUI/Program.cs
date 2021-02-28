using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using Business.CCS;

namespace ConsoleUI
{
    // SOLID
    // O : Open Closed Principle : yaptığın yazılıma yeni bir özellik ekliyorsan mevcuttaki hiçbir koduna dokunamazsın.
    class Program
    {
        static void Main(string[] args)
        {
            #region InMemoryProductDal
            //ProductManager productManager2 = new ProductManager(new InMemoryProductDal());
            //foreach (var product in productManager2.GetAll())
            //{
            //    Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            //}

            //Console.ReadLine(); 
            #endregion

            #region Entity Framework
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            OrderManager orderManager = new OrderManager(new EfOrderDal());

            GetAll(productManager);
            GetByCategoryId(productManager);
            GetByUnitPrice(productManager);

            //OrderTest(categoryManager);
            //CategoryTest(orderManager);
            ProductDetails(productManager);

            #endregion

            Console.ReadLine();
        }

        private static void ProductDetails(ProductManager productManager)
        {
            var result = productManager.GetProductDetails();

            if (result.Success) // Success == true demek
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine($"{product.ProductName} -- {product.CategoryName}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void GetByUnitPrice(ProductManager productManager)
        {
            Console.WriteLine("Fiyata göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GetByUnitPrice(50, 100).Data)
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void GetByCategoryId(ProductManager productManager)
        {
            Console.WriteLine("Kategoriye göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GetAllByCategoryId(2).Data)
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void GetAll(ProductManager productManager)
        {
            foreach (var product in productManager.GetAll().Data)
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void OrderTest(OrderManager orderManager)
        {
            foreach (var order in orderManager.GetAll().Data)
            {
                Console.WriteLine($"{order.OrderId} {order.ShipCity}");
            }
        }

        private static void CategoryTest(CategoryManager categoryManager)
        {
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine($"{category.CategoryId} -- {category.CategoryName}");
            }
        }
    }
}
