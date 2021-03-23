using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{

 public   class AccessToken //token : jwt değerinin ta kendisi , kullanıcı postmanden kullanıcı adı vericek biz de ona token vereceğiz ne zaman sonlanacağının bilgisini de vereceğiz
    {
        //token anlamsız karakterlerden oluşan anahtar değeridir o yüzden string veriyoruz
        public string Token { get; set; }
        public DateTime Expiration { get; set; }//bitiş zamanı 
    }
}
