using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    // Northwind'e özel sabit değerler burada tutulur.
    public static class Messages  // static verdik ki new()'lemeyelim.
    {
        // public yapıların hepsi Pascal Case'e göre yazılır. private olsayadı productAdded olurdu.
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string MaintenanceTime = "Sistem bakımda :(";
        public static string ProductUpdated = "Ürünler güncellendi.";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir!!";
        public static string ProductNameAlreadyExists = "Bu isimde ürün sistemde mevcut!!";
        public static string CategoryIdLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor!!";
    }
}
