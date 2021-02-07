using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Entity Framework - ORM (Object Relational Mapping) LINQ destekli çalışır. ORM veri tabanı nesneleriyle kodlar arası ilişki kurup veri tabanaı işlerini yapma sürecidir.
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        // IEnumerable, Bir koleksiyon üzerinde sorgulama yapmanıza olanak sağlar. IEnumerable data’yı çeker ve sorgulamanız var ise daha sonra bu işlemi gerçekleştirir. Data’yı Memory’de tutar ve kullanır.


        // IQueryable, Database vb.veri depolarında yapılan sorgulamlarda işlevsellik sağlar. IQueryable, şartlara göre bir query oluşturur ve bu query ile birlikte database’e gider. O şartlara göre sonuç döner. Teorik olarak hızı IEnumerable'den daha hızlı performans sağlar.

        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto { ProductId = p.ProductId, CategoryName = c.CategoryName, ProductName = p.ProductName, UnitsInStock = p.UnitsInStock };

                return result.ToList();
            }
        }
    }
}
