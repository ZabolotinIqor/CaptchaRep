using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaTraining.Model;

namespace CaptchaTraining.Services
{
    public interface ICaptchaService
    {
        Task<IEnumerable<CaptchaResponseDto>> GetListOfCaptcha();
        Task<CaptchaResponseDto> SaveCaptcha(CaptchaPostDto captchaPostDto);
    }
}