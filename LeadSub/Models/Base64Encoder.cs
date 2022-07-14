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
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    byte[] bytes = new byte[stream.Length + 32];
                    int numBytesToRead = (int)stream.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = stream.Read(bytes, numBytesRead, 32);
                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    string str = Convert.ToBase64String(bytes);
                    return $"data:image/{Path.GetExtension(file.FileName)};base64, {str}";
                }
            }
            else return "";
        }
        public static IFormFile GetIFormFile(string base64string)
        {
            if (base64string.Contains(";base64, "))
            {
                string removedStr = ";base64, ";
                int index = base64string.IndexOf(";base64, ");
                base64string = base64string.Remove(0, (index + removedStr.Length));
            }
            byte[] bytes = Convert.FromBase64String(base64string);
            MemoryStream stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, "name", "name.jpeg");
            return file;
        }
    }
}
