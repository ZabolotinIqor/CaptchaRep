using System.Threading.Tasks;
using CaptchaTraining.Model;
using CaptchaTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaTraining.Controllers
{
    [ApiController]
    public class CaptchaController: Controller
    {
        private readonly ICaptchaService _captchaService;

        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }
        [HttpGet]
        [Route("/captchas")]
        public async Task<IActionResult> GetCaptchas()
        {
            var result = await _captchaService.GetListOfCaptcha();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/saveCaptcha")]
        public async Task<IActionResult> SaveCaptcha([FromForm]CaptchaPostDto captchaPostDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _captchaService.SaveCaptcha(captchaPostDto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}