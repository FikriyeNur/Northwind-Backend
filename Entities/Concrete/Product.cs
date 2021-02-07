using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    // bir class'ın default değeri internal'dır. internal ile class'a sadece proje içinden erişilebilir. Burada katmanlı mimari yapısı uyguladığımız için bu class'a her yerden ulaşım sağlamak için public yaptık.
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
