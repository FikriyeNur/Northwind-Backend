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
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
            Console.WriteLine(" ");

            Console.WriteLine("Kategoriye göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
            Console.WriteLine(" ");

            Console.WriteLine("Fiyata göre filtrelenmiş sonuçlar: ");
            foreach (var product in productManager.GeyByUnitPrice(50,100))
            {
                Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            }
            Console.WriteLine(" ");
            Console.ReadLine();

            #region InMemoryProductDal
            //ProductManager productManager2 = new ProductManager(new InMemoryProductDal());
            //foreach (var product in productManager2.GetAll())
            //{
            //    Console.WriteLine(product.ProductName + " -- " + product.UnitPrice + " TL ");
            //}

            //Console.ReadLine(); 
            #endregion
        }
    }
}
