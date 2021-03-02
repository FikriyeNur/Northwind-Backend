using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // verdiğimiz password'un Hash'ini oluşturur.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // bu algoritmadan yararlanarak verdiğimiz password'un passwordHash ve passwordSalt oluşturacağız.
            {
                passwordSalt = hmac.Key; // her kullanıcı için bir key değeri oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        // sonradan sisteme giriş yapan birinin girdiği password'un bizim veri kaynağımızdaki passwordHash ve passwordSalt değerinin eşleşip eşleşmediğinin doğruluğunu kontrol ediyoruz. passwordHash ve passwordSalt değerlerini kendimiz verdiğimiz için out parametresi kullanmayız. 
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
