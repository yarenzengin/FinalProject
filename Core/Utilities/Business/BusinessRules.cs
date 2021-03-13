using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    //Araç old. için çıplak kalmasında sıkıntı yok
   public  class BusinessRules
    {               //List de yapabilirsin
        public static IResult Run(params IResult[] logics)//params verd.zaman Run içine parametre ol. isted. kadar IResult verebilirsin
        {
            foreach (var logic in logics)//bütün kuralları gezz
            {
                if (!logic.Success)
                {
                    return logic;//Başarısız olanı,kurala uymayanı business a gönderdik
                }
            }
            return null;
        }
    }
}
