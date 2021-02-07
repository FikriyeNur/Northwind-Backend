using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDetailDto : IDto // IEntity'den implemente etmedik çünkü bu bir veritabanı tablosu DEĞİL!! Veritabanında bir tabloya karşılık gelmez. Birkaç tablonun birkaç kolonuna karşılık gelen bir yapıdır DTO. Bir veya biden fazla join içeren kendi oluşturduğumuz mapping tablolar.
    {
        //DTO -- Data Transformation Object ()
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
