using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadSub.APIConfig
{
    public class AuthOptions
    {
        public const string ISSUER = "LeadSubUser";
        public const string AUDIENCE = "LeadSub";
        const string KEY = "APISECRETSECURITYKEY";
        public const int LIFETIME = 20;
        public static SymmetricSecurityKey GetSymetricKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
