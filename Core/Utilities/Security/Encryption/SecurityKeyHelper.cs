using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper // şifreleme olan sis.lerde her şeyi byte array formatında vermemiz gerekiyor jwt nin anlayaacağı hale getirmemiz gerekiyor
    {
                     //appsettings  de oluşturduğumuz
        public static  SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

    }
}
