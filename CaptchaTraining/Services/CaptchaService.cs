using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaptchaTraining.DbContext;
using CaptchaTraining.Model;
using CaptchaTraining.Utils;
using Microsoft.EntityFrameworkCore;

namespace CaptchaTraining.Services
{
    public class CaptchaService : ICaptchaService
    {
        private readonly CaptchaDbContext _captchaDbContext;
        private readonly IMapper _mapper;
        
        public CaptchaService(CaptchaDbContext captchaDbContext, IMapper mapper)
        {
            _captchaDbContext = captchaDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CaptchaResponseDto>> GetListOfCaptcha()
        {
            var listOfCaptchas = await _captchaDbContext.CaptchaDataSets.ToListAsync();
            return _mapper.Map<List<CaptchaResponseDto>>(listOfCaptchas);
        }

        public async Task<CaptchaResponseDto> SaveCaptcha(CaptchaPostDto captchaPostDto)
        {
            var captcha = _mapper.Map<CaptchaDataSet>(captchaPostDto);
            captcha.Pictures = await FileSaver.SaveZipFile(captchaPostDto.archive);
            captcha.CreationDate = DateTime.Now;
            await _captchaDbContext.AddAsync(captcha);
            await _captchaDbContext.SaveChangesAsync();
            return _mapper.Map<CaptchaResponseDto>(captcha);
        }
    }
}