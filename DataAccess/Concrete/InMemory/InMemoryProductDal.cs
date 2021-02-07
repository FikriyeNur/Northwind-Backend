using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        // bu listemiz bu class için global değişkendir. global değişkenlerin isimlendirmeleri genellikle "_" ile başlar. Buna isimlendirme standartı denir. (naming convention)
        List<Product> _products;

        // Bu proje çalıştığında ilk burası yani constructor metodu çalışır. (yani class'ı new'lediğimizde ilk bu fonksiyon çalışır.) Bunu yapma nedenimiz ise proje çalıştığında bellekte ürün listesi oluşturmak istememizdir.
        public InMemoryProductDal()
        {
            // Oracle, Sql Server, Postgres, MongoDb vb. veritabanlarıda ki gibi veri çektiğimizi simüle ediyoruz.
            _products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak", UnitsInStock=15, UnitPrice=15},
                new Product{ProductId=2, CategoryId=1, ProductName="Kamera", UnitsInStock=3, UnitPrice=500},
                new Product{ProductId=3, CategoryId=2, ProductName="Telefon", UnitsInStock=2, UnitPrice=1500},
                new Product{ProductId=4, CategoryId=2, ProductName="Klavye", UnitsInStock=65, UnitPrice=150},
                new Product{ProductId=5, CategoryId=2, ProductName="Mouse", UnitsInStock=1, UnitPrice=85}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // _products.Remove(product); // ÇALIŞMAZ!! Bu şekilde listeden asla silinmez. Çünkü arayüzden bir ürün (product) new'leyip buraya yolladığımız için referans adresleri aynı değildir. Bu yüzdende SİLİNMEZ!! (Eğer referans değilde değer tip yollasaydık o zaman bu şekilde silinirdi.)

            #region Uzun yöntem bunun kısa yolunu LINQ ile yapacağız.
            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}
            //_products.Remove(productToDelete);
            #endregion

            // LINQ - Language Integrated Query (Dile Gömülü Sorgulama)
            // => Lambda
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // _products içinde tek tek dolaşır ve verdiğimiz koşula göre istenilen elemanı bulur.(SingleOrDefault: tek bir eleman bulmaya yarar.)
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        //public List<Product> GetAllByCategory(int categoryId)
        //{
        //    return _products.Where(p => p.CategoryId == categoryId).ToList(); // wherede istediğimiz kadar koşul ekleyebiliriz.
        //}

        //public List<Product> GettAll()
        //{
        //    return _products;
        //}

        public List<Product> GettAll(Expression<Func<Product, bool>> filter = null)
        {
            return _products;
        }

        public void Update(Product product)
        {
            // Gönderdiğim ürün Id'sine sahip olan listedeki ürünü bul demek.
            Product productToUpdate = _products.SingleOrDefault(p => product.ProductId == product.ProductId);
            // ve LINQ sonunda istediğimiz elemanı buluyor ve bizde onu güncelliyoruz
            productToUpdate.ProductName = product.ProductName;
            product.CategoryId = product.CategoryId;
            product.UnitsInStock = product.UnitsInStock;
            product.UnitPrice = product.UnitPrice;
        }
    }
}
