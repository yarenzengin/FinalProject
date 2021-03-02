using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //IResult'ın somut sınıfı
    //getter ;return ü sağlar
    //ctor sayesinde set yapıcaz
    public class Result : IResult
    {
        

        //Result a  ctor sayesinde bir tane bool ve string göndermek istiyorsun
       //2 parametre gönderen birisi için ,tamam çalış ama aynı zamanda tek par. çalıştır
       //24.satırı da kapsıyoruz
        public Result(bool success, string message) : this(success)//result'ın tek parametreli ctoruna success i yolla
        {
            //read only'ler ctor içinde set edilebilir
            Message = message;
            //DO NOT REPEAT YOURSELF

        }
        public Result(bool success)//ctor overloading : 2 tane farklı method varmış gibi
        {
            
            Success = success;

        }

        public bool Success { get; }//read only

        public string Message { get; }
    }
}
