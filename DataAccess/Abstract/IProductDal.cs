using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // Dal - Data Access Layer (.NET) /  Dao - Data Access Object (Java)
    // interface'lerin operasyonları(metotları) default public, kendisi değil!! Kendisi internal'dır.
    public interface IProductDal
    {
        List<Product> GettAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetAllByCategory(int categoryId); // Product'ları Category'ye göre filtreleme işlemi
    }
}
