using Core.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak Class Kalmasın yani herhangi bir class ınheritance veya interface implementesyonu almıyorsa ileride problem yaşanır
    //bu yüzden bu varlıklarımızı işaretleme yani gruplandırma eğilimine gideriz.Deriz ki concrete'deki classlar veritabanı tablosuna karşılık geliyor
   public class Category : IEntity
        //IEntity category ve product ın referansını tutabiliyor
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
