using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using ValidationException = FluentValidation.ValidationException;
using Core.CrossCuttingConcerns.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        #region Comments
        // Bu noktada direk olarak herhangi bir veritabanıyla bağlantı kurmadan soyut yapıyla bağlantı kurduk. Artık buradan istediğimiz kısma ulaşabiliriz.
        // Field nesnelerinin default'u private'dır.
        // _ ile isimlendirmek naming convention'dur. Genellikle field'lar bu şekilde isimlendirilir. 
        #endregion
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        #region Comment
        // Bir methot sadece bir sonuç değeri döner. Örneğin liste yada sadece int, string gibi yapılar olur. Tek bir sonuç değeri döner. Birden fazla sonuç döndürmek istersek Encapsulation yapısını kullanırız. 
        #endregion
        [ValidationAspect(typeof(ProductValidator))] // Add metodununa ProductValidator'a göre doğrulama işlemi yaptık.
        public IResult Add(Product product)
        {
            #region Validation
            // validation => örneğin şifre 4 karakterden oluşmalı gibi kodlar doğrulama sınıfına dahildir.

            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //} 

            // YUKARIDAKİ kodların generic versiyonunu yaptık!!
            //ValidationTool.Validate(new ProductValidator(), product);
            #endregion

            #region AOP
            // Cross Cuttig Concerns : Loglama, Cache, Transcation (performans yönetimi), Authorization (yetkilendirme)            bu kodların hiçbiri tek satır bile olsa business katmanında bu şekilde olmamalı o yüzden bu yapıları AOP ile yapıcaz ve burası temiz kalmış olacak. Clean Code uygulayacağız!!

            #endregion

            // business codes
            _productDal.Add(product);

            //return new SuccessResult(); // bu şekilde mesaj vermeden tek parametreli de kullanabiliriz SuccessResult class'ını
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            // iş kodları ..
            // yetkisi var mı ? (gibi sorgular yazılır)
            // bütün kurallara uyduktan sonra listeyi ekrana döndürebiliriz.

            // Her gün saat 22'de sistemi kapatıyoruz.
            if (DateTime.Now.Hour == 10)
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
            if (DateTime.Now.Hour == 23)
            {
                // data döndürmeyiz. sadece mesaj döndürüyoruz. <List<Product>> default değeri döner oda NULL'dır.
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GettAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }
    }
}
