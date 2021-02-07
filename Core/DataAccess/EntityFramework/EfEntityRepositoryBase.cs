using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    // Entity Framework kullanarak repository base class'ı oluşturuyoruz. Hem entity hemde context tipinde generic bir class tanımladık çünkü projelerde tek değişen kısımlar oralar diğer kodlar hepsi için aynı. Artık bu genereic yapımızı CRUD ve filtreleme işlemlerini yapacağımız her yerde kullanabiliriz.
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
        // Generic Constraints : generic tiplere where ile istediğimiz kısıtlama işlemlerini yapabiliriz.
        // Kısıtlamaları yapmamızın sebebi bu kodları kullanacak olan kişilerin sadece bizim verdiğimiz bilgiler doğrultusunda kullanabilsin diye yönlendiriyoruz.
    {
        public void Add(TEntity entity)
        {
            // using: IDisposable Pettern Implementation of C# 
            using (TContext context = new TContext()) // using içine yazdığımız nesneler using bitince silinir. (bellekte fazla yer kaplamamış olur ve daha az performanslı bir çalışma yapmış oluruz.)
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GettAll(Expression<Func<TEntity, bool>> filter = null)
        {
            // eğer filtreleme işlemi yapılmamışsa veritabanındaki ilgili tablodaki tüm datayı getir.
            // eğer filtreleme işlemi yapılmışsa o filtreyi uygula ona göre datayı listele.
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                // ilk kısım arka planda bizim için: select * from Products döndürür ve onu listeler. 
                // ikinci kısımda filtreleyip döndürür bize istenilen sonucu
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
