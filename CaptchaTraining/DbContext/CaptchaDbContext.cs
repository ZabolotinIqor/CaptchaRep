

using CaptchaTraining.Model;
using Microsoft.EntityFrameworkCore;

namespace CaptchaTraining.DbContext
{
    public class CaptchaDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<CaptchaDataSet> CaptchaDataSets { get; set; }
        public CaptchaDbContext(DbContextOptions<CaptchaDbContext> options) : base(options) { }
    }
}