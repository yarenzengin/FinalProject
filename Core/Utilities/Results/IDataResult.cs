using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //interface, interfacei implemente ederse, implemente edilen interface elemanları ztn burda  
   public interface IDataResult<T> : IResult //mesajla , başarıyı hesaplamayı ztn IResult Yapıypr
        //hangi tipi döndüreceğini söyle
    {
        T Data { get;  }//ürünlerimiz 
    }
}
