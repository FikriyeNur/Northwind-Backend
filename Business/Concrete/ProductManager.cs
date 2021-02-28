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
using System.Linq;
using System.Text;
using Business.CCS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;

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
        private ICategoryService _categoryService;

        // bir entity manager, kendisi hariç başka bir dal'ı enjekte edemez!! 
        // category tablosu gerektiği için ICategoryService'i enjekte ettik.
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
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
            // Cross Cuttig Concerns : Loglama, Cache, Transcation, Performans yönetimi, Authorization (yetkilendirme)            bu kodların hiçbiri tek satır bile olsa business katmanında bu şekilde olmamalı o yüzden bu yapıları AOP ile yapıcaz ve burası temiz kalmış olacak. Clean Code uygulayacağız!!
            #endregion

            // business codes
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategory(product.CategoryId),
                    CheckIfProductNameExists(product.ProductName),
                    CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            //return new SuccessResult(); // bu şekilde mesaj vermeden tek parametreli de kullanabiliriz SuccessResult class'ını
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategory(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceded());

            if (result !=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
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

        private IResult CheckIfProductCountOfCategory(int categoryId)
        {
            var result = _productDal.GettAll(c => c.CategoryId == categoryId);

            if (result.Count >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GettAll(c => c.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count >= 15)
            {
                return new ErrorResult(Messages.CategoryIdLimitExceded);
            }

            return new SuccessResult();
        }
    }
}
