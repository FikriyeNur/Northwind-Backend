using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // params sayesinde buraya istediğimiz kadar parametre yani iş kuralı verebiliriz.
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; // iş kuralı başarısızsa kuralın kendisini döndürürüz. Oda ErrorResult döndürür.
                }
            }

            return null;
        }

        // List versiyonu
        public static List<IResult> Run2(params IResult[] logics) 
        {
            List<IResult> errorResult = new List<IResult>(); // kurala uymayanlar
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    errorResult.Add(logic);
                }
            }

            return errorResult;
        }
    }
}
