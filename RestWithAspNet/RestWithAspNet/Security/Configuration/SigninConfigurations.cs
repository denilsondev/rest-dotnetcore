using Microsoft.IdentityModel.Tokens;

namespace RestWithAspNet.Security.Configuration
{
    public class SigninConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigninCredentials { get; set; }

        public SigninConfigurations()
        {
            using (var provider = new System.Security.Cryptography.RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigninCredentials =  new Microsoft.IdentityModel.Tokens.SigningCredentials(Key, SecurityAlgorithms.HmacSha384);


        }
    }
}
