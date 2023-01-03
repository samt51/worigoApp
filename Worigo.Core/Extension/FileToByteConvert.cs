using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Worigo.Core.Extension
{
    public  static class FileToByteConvert
    {
        public static   string FromFileToByte(this IFormFile formfile)
        {
            if(formfile == null)
            {
                return null;
            }
            using var memoryStream = new MemoryStream();
            formfile.CopyToAsync(memoryStream);
            var arrayList = memoryStream.ToArray();
            return  Convert.ToBase64String(arrayList);
        }
      
    }
}
