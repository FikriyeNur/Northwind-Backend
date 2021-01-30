using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // Dal - Data Access Layer /  Dao - Data Access Object
    // interface'lerin operasyonları public kendisi değil!!
    public interface IProductDal
    {
        List<Product> GettAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetAllByCategory(int categoryId);
    }
}
