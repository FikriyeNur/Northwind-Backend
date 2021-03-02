using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
        public static string ProductUpdated = "Ürün güncellendi.";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir!!";
        public static string ProductNameAlreadyExists = "Bu isimde ürün sistemde mevcut!!";
        public static string CategoryIdLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor!!";
        public static string ProductDeleted = "Ürün silindi.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        internal static string UserNotFound = "Kullanıcı bulunamadı!";
        internal static string PasswordError = "Hatalı şifre!!";
        internal static string SuccessfulLogin = "Sisteme giriş başarılı";
        internal static string UserAlreadyExists = "Bu kullanıcı zaten mevcut!";
        internal static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";
    }
}
