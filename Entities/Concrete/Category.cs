using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    // Çıplak Class Kalmasın - ÇCK Standartı (class'ları inheritance etmezsek veya interfaceler'den implemente etmezsek ileride sıkıntılar çıkabilir.)
    public class Category : IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
