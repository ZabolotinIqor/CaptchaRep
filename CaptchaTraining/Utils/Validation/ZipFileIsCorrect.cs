using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using CaptchaTraining.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CaptchaTraining.Utils.Validation
{
    public class ZipFileIsCorrect: Attribute, IModelValidator 
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var file = context.Model as IFormFile;
            if (file == null)
            {
                return new List<ModelValidationResult>
                {
                    new ModelValidationResult(context.ModelMetadata.PropertyName, "File is null")
                };
            }
            if (context.Container is CaptchaPostDto data)
            {
                int neededMinImageCount = 2000 + countOfSelected(data) * 3000;

                using (var stream = file.OpenReadStream())
                using (var archive = new ZipArchive(stream))
                {
                    var countOfFilesWithoutAswers = zipHasFile("answers.txt", archive)
                        ? archive.Entries.Count
                        : archive.Entries.Count - 1;
                    var _countOfAnswers = countOfAnswers("answers.txt", archive);
                    if (countOfFilesWithoutAswers < neededMinImageCount || countOfFilesWithoutAswers > neededMinImageCount + 1000 )
                    {
                        return new List<ModelValidationResult>
                        {
                            new ModelValidationResult(context.ModelMetadata.PropertyName, $"Count of images out of range ({neededMinImageCount}-{neededMinImageCount+1000})")
                        };
                    }
                    if (_countOfAnswers != archive.Entries.Count -1)
                    {
                        return new List<ModelValidationResult>
                        {
                            new ModelValidationResult(context.ModelMetadata.PropertyName, $"Count of answers({_countOfAnswers}) not equal count of images {archive.Entries.Count-1}")
                        };
                    }
                    if (!zipHasFile("answers.txt",archive))
                    {
                        return new List<ModelValidationResult>
                        {
                            new ModelValidationResult(context.ModelMetadata.PropertyName, "Archive does not exist answer.txt")
                        };
                    }
                }
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
        private static bool zipHasFile(string fileFullName, ZipArchive archive)
        {
            return archive.Entries.Any(entry => entry.FullName.EndsWith(fileFullName, StringComparison.OrdinalIgnoreCase));
        }

        private static int countOfSelected(CaptchaPostDto captchaPostDto)
        {
            var booleanValues = captchaPostDto.GetType().GetProperties().Where(p => p.PropertyType == typeof(bool));
            var res = booleanValues.Where(p => Convert.ToBoolean(p.GetValue(captchaPostDto)) && p.Name != "hasAnswers");
            return res.Count();
        }

        private static int countOfAnswers(string fileFullName, ZipArchive archive)
        {
           var entry =  archive.Entries.FirstOrDefault(entry => entry.FullName.EndsWith(fileFullName, StringComparison.OrdinalIgnoreCase));
           var res = 0;
           if (entry != null)
           {
               entry.ExtractToFile(Path.Combine(Directory.GetCurrentDirectory(), @"Files", entry.FullName));
               DirectoryInfo di = new DirectoryInfo( Path.Combine(Directory.GetCurrentDirectory(), @"Files"));

               foreach (var fi in di.GetFiles("answers.txt"))
               {
                   res = File.ReadLines(fi.FullName).Count();
                   File.Delete(fi.FullName);
               }
               
           }
           
           return res;
        }
        
        
    }
}