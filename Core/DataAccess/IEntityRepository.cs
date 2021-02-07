using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

 // Core katmanı diğer katmanları referans almaz. Kimseye bağımlı OLMAZ!!
 // Core katmanı benim evrensel katmanım olacak. Bütün .Net projelerimde bu projeyi kullanabilirim. Bu sayede kod tekrarında kurtulmuş olacağım. Bütün yapılar generic olarak yapıldı.
namespace Core.DataAccess                         
{
    // Generic Constraint: Generic kısıtlama. Bu generic yapıya herkes ulaşamasın diye yani sadece veri tabanı class'larım bu interface'i kullanabilsin diye buraya filtre koydum. 
    // Artık burası sadece referans tip olabilir. IEntity veya IEntity'i implemente eden bir nesne olabilir!!
    // new() : new'lenebilir olmalı demektir. IEntity new'lenemediği için artık buraya sadece IEntity'i iplemente eden nesneler gelebilir.
    // class: referans tip 
    public interface IEntityRepository<T> where T : class, IEntity, new() // Generic Repository yaptık. Her class'da bu işlemleri tekrar etmemek için bu generic yapıyı oluşturduk.      
    {
        List<T> GettAll(Expression<Func<T, bool>> filter = null); // delege. filtre vermediğimiz için tüm datayı çekeriz.
        T Get(Expression<Func<T, bool>> filter); // filter=null yapmadık çünkü filtre vermek zorundayız. Çünkü bu fonksiyonda filtreye göre bir şeyler çekmek istiyoruz.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);   
    }
}
