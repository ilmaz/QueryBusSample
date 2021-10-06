using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QueryBusSampleConsole.Framework.CqrsValidation
{
    public sealed class CqrsValidationException : Exception
    {
        public CqrsValidationException(IEnumerable<ValidationResult> validationResults)
        {
            Errors = validationResults.Select(x => new KeyValuePair<string, string>(x.MemberNames.First(), x.ErrorMessage))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, string> Errors { get; }
    }
}