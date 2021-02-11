using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result // Result class'ından inherit ettik.
    {
        public SuccessResult(string message) : base(true, message) // base Result'a bir şey yollama anlamına gelir.
        {

        }

        public SuccessResult() : base(true) // base -- Result class'ıdır. onun içindeki constructorları kullanıyoruz. ( base'de iki parametreli consturctor var. )
        {

        }
    }
}
