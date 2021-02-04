using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // Dal - Data Access Layer (.NET) /  Dao - Data Access Object (Java)
    // interface'lerin operasyonları(metotları) default public, kendisi değil!! Kendisi internal'dır.
    public interface IProductDal : IEntityRepository<Product> // IEntityRepository'i Product için oluşturduk. Her class için ortak olan özellikleri bu interface ile tekrar etmeyi bıraktık.
    {

    }
}
