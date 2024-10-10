using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName) 
        {
            //var folderPath = @"D:\Courses\Route\Company.Web\Company.Web\wwwroot\files\images\";

            //1.Get Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//files", folderName);
            //2.Get file name
            var fileName = $"{Guid.NewGuid}-{file.FileName}";
            //3.combine folder path with file name
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return filePath;

        }
    }
}
