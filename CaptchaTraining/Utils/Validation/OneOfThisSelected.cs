using System;
using System.Collections.Generic;
using System.Linq;
using CaptchaTraining.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CaptchaTraining.Utils.Validation
{
    public class OneOfThisSelected: Attribute, IModelValidator 
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {

            if (context.Container is CaptchaPostDto data)
            {
                if (data.hasCyrillic || data.hasLatin || data.hasNumeric)
                {
                    return Enumerable.Empty<ModelValidationResult>();
                }

            }
            return new List<ModelValidationResult>
            {
                new ModelValidationResult(context.ModelMetadata.PropertyName, "One of: Has Numeric, Has Cyrillic, Has Latin must be selected")
            };
        }
    }
}