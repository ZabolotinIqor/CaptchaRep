using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CaptchaTraining.Utils
{
    public  class FileSaver
    {
        public static async Task<string> SaveZipFile(IFormFile file)
        {
            if (file == null || file.Length <= 0) return "";
            
            var fileName = file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Files", fileName); 
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;

        }
    }
}