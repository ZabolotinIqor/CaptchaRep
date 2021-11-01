using CaptchaTraining.Utils.Validation;
using Microsoft.AspNetCore.Http;

namespace CaptchaTraining.Model
{
    public class CaptchaPostDto
    {
        [OnlyLatinAndNoCaptcha]
        public string Name { get; set; }
        public bool hasCyrillic { get; set; }
        public bool hasLatin { get; set; }
        public bool hasNumeric { get; set; }
        public bool hasSpecialSymbols { get; set; }
        public bool isCaseSesitive { get; set; }
        public bool hasAnswers { get; set; }
        [ZipFileIsCorrect]
        public IFormFile archive { get; set; }
    }
}