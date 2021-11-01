using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
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

            if (context.Container is CaptchaPostDto {hasAnswers: true})
            {
                using (var stream = file.OpenReadStream())
                using (var archive = new ZipArchive(stream))
                {
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
        
    }
}