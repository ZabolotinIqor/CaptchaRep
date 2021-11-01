using System;

namespace CaptchaTraining.Model
{
    public class CaptchaDataSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool hasCyrillic { get; set; }
        public bool hasLatin { get; set; }
        public bool hasNumeric { get; set; }
        public bool hasSpecialSymbols { get; set; }
        public bool isCaseSesitive { get; set; }
        public bool hasAnswers { get; set; }
        public string Pictures { get; set; }
    }
}