using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
    public class FileBusiness : IFileBusiness
    {
        public byte[] GetPdfFile()
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                var fullPath = path + "\\Other\\teste-pdf.pdf";
                return File.ReadAllBytes(fullPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
