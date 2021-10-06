using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QueryBusSampleConsole.Framework.Base;

namespace QueryBusSampleConsole.Framework.CqrsValidation
{
    internal class CqrsValidationContext
    {
        public static void ValidateQuery<TResponse>(IQuery<TResponse> query)
        {

            var validationContext = new ValidationContext(query, null, null);
            var result = new List<ValidationResult>();

            if (Validator.TryValidateObject(query, validationContext, result)) return;

            throw new CqrsValidationException(result);
        }
    }
}