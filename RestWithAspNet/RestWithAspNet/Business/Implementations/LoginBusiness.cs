using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using RestWithAspNet.Data.Converters;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Repository.Implementations;
using RestWithAspNet.Security.Configuration;

namespace RestWithAspNet.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private IUserRepository _repository;
        private SigninConfigurations _signinConfigurations;
        private TokenConfiguration _tokenconfiguration;

        public LoginBusiness(IUserRepository repository, SigninConfigurations signinConfigurations, TokenConfiguration tokenconfiguration)
        {
            _repository = repository;
            _signinConfigurations = signinConfigurations;
            _tokenconfiguration = tokenconfiguration;
        }

        public object FindByLogin(User user)
        {
            bool credentialsIsValid = false;
            if(user != null && !string.IsNullOrEmpty(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }
            if(credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                    );

                DateTime createdDate = DateTime.Now;
                DateTime expirationDate = createdDate + TimeSpan.FromSeconds(_tokenconfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createdDate, expirationDate, handler);

                return SucessObject(createdDate, expirationDate, token);
            } else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createdDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenconfiguration.Issuer,
                Audience = _tokenconfiguration.Audience,
                SigningCredentials = _signinConfigurations.SigninCredentials,
                Subject = identity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SucessObject(DateTime createdDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createdDate.ToString("yyyy/MM/dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy/MM/dd HH:mm:ss"),
                AcessToken = token,
                message = "Ok"
            };
        }
    }
}
