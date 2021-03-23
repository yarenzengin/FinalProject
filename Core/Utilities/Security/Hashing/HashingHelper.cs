using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    //araç
  public  class HashingHelper
    {
        //verdiğin bir password değerinin salt ve hash değerini oluşturmaya yarıyor
        public static void CreatePasswordHash(string password , out byte[] passwordHash , out byte[] passwordSalt)
        {//saltı da içinde bulundurucak
            using (var hmac = new System.Security.Cryptography.HMACSHA512())//salt ve hash oluşturucaz
            {
                
                //out kullandığımız için buraya passwordHash ve saltı yazdığımızda döndürmüş olucaz
                passwordSalt = hmac.Key;//key algoritmanın değişmeyen anahtarı , bild. yapı şifrelerimiz için,her kullanıcı için bir key oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//string in byte karşılığını almak için
            }
        }                             //kullanıcının verdi.parola  //vtdeki hash   ile verd. salta göre eşleşip eşleşmediğini verd. yerdir
        public static bool VerifyPasswordHash(string password, byte[] passwordHash,  byte[] passwordSalt) //out vermiyoruz çünkü bu değerleri biz veriyoruz
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //doğrulama yapacağımız için Key istiyor bu da Salt aslında
            {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//hesaplanan hash ,salt kullnarak yapılıyor
                for (int i = 0; i < computedHash.Length; i++)//dizi old. için -- byte[]
                {
                    if (computedHash[i] != passwordHash[i]) //karşılaştırma yapıyoruz
                    {
                        return false;
                    }
                }
              
            }
            return true;
        }
    }
}
