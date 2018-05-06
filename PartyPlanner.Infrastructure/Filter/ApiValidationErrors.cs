using System;
using System.Collections.Generic;
using System.Linq;

namespace PartyPlanner.Infrastructure.Filter
{
    [Serializable]
    public class ApiValidationErrors
    {
        public IList<ValidationError> ValidationErrors { get; set; }

        public ApiValidationErrors(IEnumerable<string> errorTexts)
        {
            ValidationErrors = errorTexts.Select(text => new ValidationError(text)).ToList();
        }

        public ApiValidationErrors(string errorText)
        {
            ValidationErrors = new List<ValidationError>
            {
                new ValidationError(string.Empty, errorText)
            };
        }
        public ApiValidationErrors(string key, string errorText)
        {
            ValidationErrors = new List<ValidationError>
            {
                new ValidationError(key, errorText)
            };
        }

        public ApiValidationErrors(IEnumerable<ValidationError> errors)
        {
            ValidationErrors = errors.ToList();
        }
    }
}
