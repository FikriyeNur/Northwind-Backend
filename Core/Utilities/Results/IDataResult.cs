using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Return (data) tipi metotlar için interface. IDataResult hem işlem sonucunu hem de mesajı içeren ve return tipi olan bir yapıdır.
    public interface IDataResult<T> : IResult // DRY prensibi gereği kendimizi tekrar etmiyoruz. Mesaj ve işlem sonucu IResault da mevcut ondan implemente ettik bu interface'i. Burada ekstra data (ürün, ürünler) property'sini oluşturuyoruz.
    {
        T Data { get; }
    }
}
