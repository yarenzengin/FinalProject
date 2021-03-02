using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    //sürekli new lememek için static verdik
  public  static  class Messages
    {
        //publicler büyük harfle yazılır
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime ="Sistem bakımda" ;
        public static string ProductsListed = "Ürünler listelendi";
    }
}
