using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Utilities -- Araçlar
    // Temel void metotlar için başlangıç interface'i.
    public interface IResult 
    {
        // sadece okunabilir özellikte property oluşturduk.
        // Void yapısının Inrerface'ini oluşturduğumuz için bir sonuç döndürmesi beklenmez sadece işlem sonucu ve mesajını döndürmesi yeterli ilk etapta.
        bool Success { get; } 
        string Message { get; }
    }
}
