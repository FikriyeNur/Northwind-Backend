using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Entity Framework - ORM (Object Relational Mapping) LINQ destekli çalışır. ORM veri tabanı nesneleriyle kodlar arası ilişki kurup veri tabanaı işlerini yapma sürecidir.
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            // using: IDisposable Pettern Implementation of C# 
            using (NorthwindContext context = new NorthwindContext()) // using içine yazdığımız nesneler using bitince silinir. (bellekte fazla yer kaplamamış olur ve daha az performanslı bir çalışma yapmış oluruz.)
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GettAll(Expression<Func<Product, bool>> filter = null)
        {
            // eğer filtreleme işlemi yapılmamışsa veritabanındaki ilgili tablodaki tüm datayı getir.
            // eğer filtreleme işlemi yapılmışsa o filtreyi uygula ona göre datayı listele.
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();
                // ilk kısım arka planda bizim için: select * from Products döndürür ve onu listeler. 
                // ikinci kısımda filtreleyip döndürür bize istenilen sonucu
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
