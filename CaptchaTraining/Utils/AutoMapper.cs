using AutoMapper;
using CaptchaTraining.Model;

namespace CaptchaTraining.Utils
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap<CaptchaResponseDto, CaptchaDataSet>();
            CreateMap<CaptchaDataSet, CaptchaResponseDto>();
            CreateMap<CaptchaPostDto, CaptchaDataSet>();
            CreateMap<CaptchaDataSet, CaptchaPostDto>();
            
        }
    }
}