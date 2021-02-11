using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message) // data ve mesaj veriyoruz.
        {

        }

        public ErrorDataResult(T data) : base(data, false) // sadece data veriyoruz.
        {

        }

        // az kullanılan versiyon sadece mesaj dönen constructor. T tipinin yani datanın default halini döndürürüz.
        public ErrorDataResult(string message) : base(default, false, message) // sadece mesaj veriyoruz. (default data'ya karşılık gelir. Verilen tipin default deperi döner. Örneğin string ise onun default değeri gibi.)
        {

        }

        // az kullanılan versiyon sadece data tipinin default değeri döner.
        public ErrorDataResult() : base(default, false) // hiç bir şey vermediğimiz return türü.
        {

        }
    }
}
