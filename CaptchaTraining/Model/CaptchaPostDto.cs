using System.ComponentModel.DataAnnotations;
using CaptchaTraining.Utils.Validation;
using Microsoft.AspNetCore.Http;

namespace CaptchaTraining.Model
{
    public class CaptchaPostDto
    {
        [MinLength(4)]
        [MaxLength(8)]
        [OnlyLatinAndNoCaptcha]
        public string Name { get; set; }
        [OneOfThisSelected]
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