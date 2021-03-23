using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
  public  class SigningCredentialsHelper
    {
        //Gelen jwt yi WebApi nin doğrulaması lazım
        //bizim için jwt de webapinin kullanabileceği jwt oluşturuabilmesi için , Credentialdır : sisteme girebilmek için gerekli olan anahtarımız 
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //asp.net 'e : sen bir hashing işlemi yapıcaksın anahtar ol. sec.Key i kullan , şifereleme ol . güvenlik algo.dan HmacSha512 i kullan  
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
