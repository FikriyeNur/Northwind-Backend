using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

// Wep API: Angular, IOS, Android gibi UI'ların (kullanıcıların) projemizi kullanabilmesi (anlaması) için bu standart yapıyı kullanırız. Wbp API Restful service'dir. Genellikle JSON formatıyla çalışır. Wep API HTTP istekleri üzerinden yapılır. Kullanıcılar bize Request (istekler)'de bulunurlar. Örneğin Ürünleri listele gibi. Bizim ona verdiğimiz yanıt Response (cevap)'dur.

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; // Bu noktada direk olarak herhangi bir veritabanıyla bağlantı kurmadan soyut yapıyla bağlantı kurduk. Artık buradan istediğimiz kısma ulaşabiliriz.

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product) // Bir methot sadece bir sonuç değeri döner. Örneğin liste yada sadece int, string gibi yapılar olur. Tek bir sonuç değeri döner. Birden fazla sonuç döndürmek istersek Encapsulation yapısını kullanırız.
        {
            // business codes
            // ürünü eklemeden önceki kodlar yazılır. ürün ekleme koşulları

            if (product.ProductName.Length < 2)
            {
                // magic strings
                //return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır!");
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);

            //return new SuccessResult(); // bu şekilde mesaj vermeden tek parametreli de kullanabiliriz SuccessResult calss'ını
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            // iş kodları ..
            // yetkisi var mı ? (gibi sorgular yazılır)
            // bütün kurallara uyduktan sonra listeyi ekrana döndürebiliriz.

            // Her gün saat 22'de sistemi kapatıyoruz.
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime); // data döndürmeyiz. sadece mesaj döndürüyoruz. <List<Product>> default değeri döner oda NULL'dır.
            }

            return new SuccessDataResult<List<Product>>(_productDal.GettAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id) // filtrelenmiş ürün listesi
        {
            return new SuccessDataResult<List<Product>>(_productDal.GettAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 2)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime); // data döndürmeyiz. sadece mesaj döndürüyoruz. <List<Product>> default değeri döner oda NULL'dır.
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GettAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }
    }
}
