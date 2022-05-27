using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeadSub.Models
{
    public class Base64Encoder
    {
        public static string GetBase64String(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                byte[] bytes=new byte[stream.Length+32];
                int numBytesToRead = (int)stream.Length;
                int numBytesRead = 0;
                while (numBytesToRead>0)
                {
                    int n = stream.Read(bytes, numBytesRead, 32);
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                string str=Convert.ToBase64String(bytes);
                return $"data:image/{Path.GetExtension(file.FileName)};base64, {str}";
            }
        }
    }
}
