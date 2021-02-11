using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message) // data ve mesaj veriyoruz.
        {

        }

        public SuccessDataResult(T data) : base(data, true) // sadece data veriyoruz.
        {

        }

        // az kullanılan versiyon sadece mesaj dönen constructor. T tipinin yani datanın default halini döndürürüz.
        public SuccessDataResult(string message) : base(default, true, message) // sadece mesaj veriyoruz. (default data'ya karşılık gelir. Verilen tipin default deperi döner. Örneğin string ise onun default değeri gibi.)
        {

        }

        // az kullanılan versiyon sadece data tipinin default değeri döner.
        public SuccessDataResult():base(default, true) // hiç bir şey vermediğimiz return türü.
        {

        }
    }
}
