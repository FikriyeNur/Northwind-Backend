using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; // Bu noktada direk olarak herhangi bir veritabanıyla bağlantı kurmadan soyut yapıyla bağlantı kurduk. Artık buradan istediğimiz kısma ulaşabiliriz.

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            // iş kodları ..
            // yetkisi var mı ? (gibi sorgular yazılır)
            // bütün kurallara uyduktan sonra listeyi ekrana döndürebiliriz.
            return _productDal.GettAll();
        }

        public List<Product> GetAllByCategoryId(int id) // filtrelenmiş ürün listesi
        {
            return _productDal.GettAll(p => p.CategoryId == id);

        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public List<Product> GeyByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GettAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }
    }
}
