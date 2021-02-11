using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult // Result IResult interface'inden implemente oldu.
    {
        // Sadece okunabilir "Get" property'lere constructor içinde "set" değer atama işlemi yapabiliriz. 
        public Result(bool success, string message) : this(success) // this class'ın kendisi demektir. Result'ın tek parametreli constructor'una success parametresini yolladık. Artık success değeri işlemleri aşağıdaki constructorda yapılacak. Yani iki parametre gönderen birisi için bu constructor çalışır ama Success kontrol işlemi aşağıdaki constructor'dan alınır.
        {
            Message = message;
        }

        // DRY -- Don't Repat Yourself!!
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}