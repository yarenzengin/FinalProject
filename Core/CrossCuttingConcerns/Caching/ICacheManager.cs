using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public    interface ICacheManager//bütün alternatiflerin interfacei 
    {
        T Get<T>(string key);//T: farklı veri tipleri döndrcez, generic metotta verebiliriz
        void Add(string key, object value, int duration);
        object Get(string key);
        bool IsAdd(string key);//cache de var mı
        void Remove(string key);//uçur
        void RemoveByPattern(string pattern);//örn: başı sonu önemli eğil içinde get olanları uçur vb 


    }
}