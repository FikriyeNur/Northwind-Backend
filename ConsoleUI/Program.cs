using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

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
            //GetAll(productManager);
            //GetByCategoryId(productManager);
            //GetByUnitPrice(productManager);

            //OrderTest();

            //CategoryTest();

            ProductManager productManager = new ProductManager(new EfProductDal());

            ProductDetails(productManager);

            #endregion

            Console.ReadLine();
        }

        private static void ProductDetails(ProductManager productManager)
        {
            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine($"{product.ProductName} -- {product.CategoryName}");
            }
        }

        private static void GetByUnitPrice(ProductManager productManager)
        {
            Console.WriteLine("Fiyata göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GeyByUnitPrice(50, 100))
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void GetByCategoryId(ProductManager productManager)
        {
            Console.WriteLine("Kategoriye göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void GetAll(ProductManager productManager)
        {
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
        }

        private static void OrderTest()
        {
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            foreach (var order in orderManager.GetAll())
            {
                Console.WriteLine($"{order.OrderId} {order.ShipCity}");
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine($"{category.CategoryId} -- {category.CategoryName}");
            }
        }
    }
}
